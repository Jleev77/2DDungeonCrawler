using System;
using System.Collections.Generic;
namespace Assets.DataFramework
{
    class EntityManager : IEntityManager
    {
        private int _entitySequence;
        private Dictionary<int, IEntity> _entities = new Dictionary<int, IEntity>();
        private Dictionary<Type, Dictionary<int, IComponent>> _componentsByClass = new Dictionary<Type, Dictionary<int, IComponent>>();

        public bool AddEntity(IEntity entity)
        {
            entity.Id = generateId();
            if(entity.Id == int.MinValue){ return false; }

            _entities.Add(entity.Id, entity);
            return true;
        }

        public void RemoveEntity(int entityId)
        {
            _entities.Remove(entityId);
        }

        public bool AddComponent(int entityId, IComponent component)
        {
            if (!entityExists(entityId)){ return false; }

            var components = _componentsByClass[component.GetType()];

            if(components == null)
            {
                components = new Dictionary<int, IComponent>();
                _componentsByClass.Add(component.GetType(), components);
            }

            if(components[entityId] != null) { return false; }

            components.Add(entityId, component);
            return true;


        }

        public void RemoveComponent(int entityId, Type componentType)
        {
            if (!componentType.IsSubclassOf(typeof(IComponent))) { return; }
            if (!entityExists(entityId)) { return; }

            var components = _componentsByClass[componentType];
            if(components != null)
            {
                components.Remove(entityId);
            }
        }

        public Dictionary<int, IComponent> GetComponents(Type componentType)
        {
            if (!componentType.IsSubclassOf(typeof(IComponent))) { return null; }
            return _componentsByClass[componentType];
        }

        private bool entityExists(int entityId)
        {
            return _entities[entityId] != null;
        }

        private int generateId()
        {
            if(_entitySequence < int.MaxValue)
            {
                return _entitySequence++;
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                if (!entityExists(i))
                {
                    return i;
                }
            }

            //NO MORE ENTITY ID's LEFT!!! AHHHHHHHHHH!
            return int.MinValue;
        }
    }
}
