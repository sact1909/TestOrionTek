using System;
using System.Collections.Generic;
using System.Text;

namespace TestOrionTek.ApiSettings
{
    public enum Priority
    {
        Background,
        User,
        Speculative,
        Explicit,
    }

    public static class PriorityExtensions
    {
        public static Fusillade.Priority ToFusilladePriority(this Priority priority)
        {
            switch (priority)
            {
                case Priority.Background: return Fusillade.Priority.Background;
                case Priority.User: return Fusillade.Priority.UserInitiated;
                case Priority.Speculative: return Fusillade.Priority.Speculative;
                case Priority.Explicit: return Fusillade.Priority.Explicit;
            }

            return Fusillade.Priority.Speculative;
        }
    }
}
