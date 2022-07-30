using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRObjectLibrary : MonoBehaviour
    {
        public List<CVRObjectCatalogCategory> objectCatalogCategories = new List<CVRObjectCatalogCategory>();
        public List<CVRObjectCatalogEntry> objectCatalogEntries = new List<CVRObjectCatalogEntry>();
    }
}