using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Halcyon
{
    public class StringListSearchProvider :ScriptableObject, ISearchWindowProvider
    {
        private string title;
        private List<string> items;

        private Action<int> onSetIndexCallback;
        public void Initialize(string title, List<string> items, Action<int> onSetIndexCallback)
        {
            this.title = title;
            this.items = items;
            this.onSetIndexCallback = onSetIndexCallback;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
            searchList.Add(new SearchTreeGroupEntry(new GUIContent(title),0));
            
            for (var index = 0; index < items.Count; index++)
            {
                
                var item = items[index];
                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(item));
                entry.level = 1;
                entry.userData = index;
                searchList.Add(entry);
            }
            
            return searchList;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            onSetIndexCallback?.Invoke((int)SearchTreeEntry.userData);
            return true;
        }
    }
}