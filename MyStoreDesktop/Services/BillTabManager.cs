using MyStoreDesktop.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyStoreDesktop.Services
{
    public class BillTabManager
    {
        private const int MAX_TABS = 5;
        private Dictionary<TabPage, BillSessionState> _tabSessions;
        private int _nextTabNumber = 1;

        public BillTabManager()
        {
            _tabSessions = new Dictionary<TabPage, BillSessionState>();
        }

        public BillSessionState GetSessionForTab(TabPage tab)
        {
            if (tab == null || !_tabSessions.ContainsKey(tab))
                return null;

            return _tabSessions[tab];
        }

        public BillSessionState CreateNewSession()
        {
            var state = new BillSessionState(_nextTabNumber++);
            return state;
        }

        public void RegisterTab(TabPage tab, BillSessionState state)
        {
            if (tab != null && state != null)
            {
                _tabSessions[tab] = state;
            }
        }

        public void RemoveTab(TabPage tab)
        {
            if (tab != null && _tabSessions.ContainsKey(tab))
            {
                _tabSessions.Remove(tab);
            }
        }

        public bool CanAddMoreTabs()
        {
            return _tabSessions.Count < MAX_TABS;
        }

        public int GetActiveTabCount()
        {
            return _tabSessions.Count;
        }
    }
}
