using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataFramework
{
    interface IEntityManager
    {
        bool AddEntity(IEntity entity);
        void RemoveEntity(int entityId);
        bool AddComponent(int entityId, IComponent component);
        void RemoveComponent(int entityId, Type component);
        Dictionary<int, IComponent> GetComponents(Type componentType);
    }
}
