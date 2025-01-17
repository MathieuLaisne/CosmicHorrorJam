using Exion.Ataraxia.Handler;
using Exion.Ataraxia.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Exion.Ataraxia.Default
{
    public class Building
    {
        private string m_name;

        public string Name
        {
            get { return m_name; }
        }

        private BuildingType m_type;

        public BuildingType Type
        {
            get { return m_type; }
        }

        private List<StatusHandler> m_status;

        public List<StatusHandler> Statuses
        {
            get { return m_status; }
        }

        [SerializeField]
        private List<Character> m_residents;

        public List<Character> Residents
        {
            get { return m_residents; }
        }

        private List<Character> m_workers;

        public List<Character> Workers
        {
            get { return m_workers; }
        }

        private GameObject m_building;

        public GameObject Structure
        {
            get { return m_building; }
        }

        public Building(string name, BuildingType type)
        {
            m_name = name; m_type = type;
            m_status = new List<StatusHandler>();
            if (type.hasRes) m_residents = new List<Character>();
            if (type.hasWorker) m_workers = new List<Character>();
            m_building = type.building;
        }

        public void Destroy()
        {
            if (m_type.destroyable)
            {
                if (m_type.hasWorker)
                {
                    foreach (Character worker in m_workers)
                    {
                        worker.ApplyStatus(new Status("Jobless", "Have no work, will lose health and mental strength over time."), 1);
                    }
                }
                m_type = new BuildingType("Construction Site", false, true, false);
                m_workers = new List<Character>();
            }
        }

        public void ApplyStatus(Status status, int stack)
        {
            bool toAdd = true;
            foreach (StatusHandler SH in m_status)
            {
                if (SH.status == status)
                {
                    SH.stacks += stack;
                    toAdd = false;
                }
            }
            if (toAdd) m_status.Add(new StatusHandler(status, stack));
        }

        public void AddResident(Character c)
        {
            m_residents.Add(c);
        }

        public void AddWorker(Character c)
        {
            m_workers.Add(c);
        }

        public bool CanBeWorkedBy(Job job)
        {
            foreach (Job j in m_type.workerType)
            {
                if (j == job) return true;
            }
            return false;
        }

        public bool HasStatus(string name)
        {
            if (m_status.Find(n => n.status.name == name) != null) return true;
            return false;
        }

        public int AmountStatus(string name)
        {
            StatusHandler SH = m_status.Find(n => n.status.name == name);
            if(SH != null)
            {
                return SH.stacks;
            }
            return 0;
        }

        public void DamageWorkers(int amount)
        {
            foreach (Character c in m_workers)
            {
                c.DealHealthDamage(amount);
            }
        }

        public void DamageResidents(int amount)
        {
            foreach(Character c in m_residents)
            {
                c.DealHealthDamage(amount);
            }
        }

        public void RemoveStatus(string name)
        {
            m_status.Remove(m_status.Find(n => n.status.name == name));
        }
    }
}