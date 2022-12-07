using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;
using System.Linq;

public enum ProjectileTag {Player, Enemy, NPC }

public class ProjectilePool : Singleton<ProjectilePool>
{
    [Serializable]
    public class PoolGroupProfile
    {
        [SerializeField] private string groupName;
        [SerializeField] private ProjectileTag groupTag;
        [SerializeField] private GameObject groupPrefab;
        [SerializeField] private int poolLenght;

        private List<GameObject> poolObjects = new List<GameObject>();

        public List<GameObject> PoolObjects { get => poolObjects; private set => poolObjects = value; }
        public ProjectileTag GroupTag { get => groupTag; private set => groupTag = value; }
        public string GroupName { get => groupName; private set => groupName = value; }

        public  void CreatePool(Transform container)
        {
            for (int i = 0; i < poolLenght; i++)
            {
                GameObject instance = Instantiate(groupPrefab,container);
                instance.name = GroupName + i;
                PoolObjects.Add(instance);
                instance.gameObject.SetActive(false);
            }
        }     
    }

    [Header("Settings")]
    [SerializeField] private List<PoolGroupProfile> poolProfiles;

    // Start is called before the first frame update
    void Start()
    {
        CreatePools();
    }

    public void CreatePools()
    {
        for (int i = 0; i < poolProfiles.Count; i++)
        {
            GameObject container = new GameObject();
            container.name = poolProfiles[i].GroupName + " Container";
            poolProfiles[i].CreatePool(container.transform);
        }
    }

    public GameObject GetItem(ProjectileTag tag)
    {
        GameObject obj = null;
        PoolGroupProfile p = poolProfiles.FirstOrDefault(profile => profile.GroupTag == tag);

        if (p != null)
        {
            for (int i = 0; i < p.PoolObjects.Count; i++)
            {
                GameObject poolObj = p.PoolObjects[i];

                //Return the first inactive object finded
                if (!poolObj.activeInHierarchy)
                {
                    obj = poolObj;
                    obj.SetActive(true);
                    return obj;
                }
            }

            //If all objects are active return the first one
            obj = p.PoolObjects[0];
        }
        else
        {
            Debug.LogError("Pool group dont exist");
        }

        obj.SetActive(true);
        return obj;
    }
}
