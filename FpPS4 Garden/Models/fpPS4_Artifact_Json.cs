using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FpPS4_Garden.Models
{
    public class fpPS4_Artifact_Json
    {
        public class Artifact
        {
            public int id { get; set; }
            public string node_id { get; set; }
            public string name { get; set; }
            public int size_in_bytes { get; set; }
            public string url { get; set; }
            public string archive_download_url { get; set; }
            public bool expired { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime expires_at { get; set; }
            public WorkflowRun workflow_run { get; set; }
        }

        public class WorkflowRun
        {
            public object id { get; set; }
            public int repository_id { get; set; }
            public int head_repository_id { get; set; }
            public string head_branch { get; set; }
            public string head_sha { get; set; }
        }

        public class Root
        {
            public int total_count { get; set; }
            public List<Artifact> artifacts { get; set; }
        }
    }
}
