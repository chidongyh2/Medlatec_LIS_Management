using System;
using System.Collections.Generic;
using System.Text;

namespace LIS.Infrastructure.Models
{
    public class TreeData
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string IdPath { get; set; }
        public dynamic Data { get; set; }
        public State State { get; set; }
        public List<TreeData> Children { get; set; }
        public int? ChildCount { get; set; }

        public TreeData()
        {
            Icon = string.Empty;

            State = new State
            {
                Opened = false,
                Selected = false,
                Disabled = false
            };
        }
    }

    public class TreeDataIdString
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string IdPath { get; set; }
        public dynamic Data { get; set; }
        public State State { get; set; }
        public List<TreeDataIdString> Children { get; set; }
        public int? ChildCount { get; set; }

        public TreeDataIdString()
        {
            Icon = string.Empty;
            Children = new List<TreeDataIdString>();
            State = new State
            {
                Opened = false,
                Selected = false,
                Disabled = false
            };
        }
    }

    public class State
    {
        public bool? Opened { get; set; }
        public bool? Selected { get; set; }
        public bool? Disabled { get; set; }

        public State()
        {
            Opened = false;
            Selected = false;
            Disabled = false;
        }
    }
}
