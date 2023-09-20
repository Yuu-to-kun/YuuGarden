using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Models
{
    public class fpPS4_Github_Classes
    {
        public class fpPS4_Workflow
        {
            public class WorkFlow_Actor
            {
                public string login { get; set; }
                public int id { get; set; }
                public string node_id { get; set; }
                public string avatar_url { get; set; }
                public string gravatar_id { get; set; }
                public string url { get; set; }
                public string html_url { get; set; }
                public string followers_url { get; set; }
                public string following_url { get; set; }
                public string gists_url { get; set; }
                public string starred_url { get; set; }
                public string subscriptions_url { get; set; }
                public string organizations_url { get; set; }
                public string repos_url { get; set; }
                public string events_url { get; set; }
                public string received_events_url { get; set; }
                public string type { get; set; }
                public bool site_admin { get; set; }
            }

            public class WorkFlow_Author
            {
                public string name { get; set; }
                public string email { get; set; }
            }

            public class WorkFlow_Committer
            {
                public string name { get; set; }
                public string email { get; set; }
            }

            public class WorkFlow_HeadCommit
            {
                public string id { get; set; }
                public string tree_id { get; set; }
                public string message { get; set; }
                public DateTime timestamp { get; set; }
                public WorkFlow_Author author { get; set; }
                public WorkFlow_Committer committer { get; set; }
            }

            public class WorkFlow_HeadRepository
            {
                public int id { get; set; }
                public string node_id { get; set; }
                public string name { get; set; }
                public string full_name { get; set; }
                public bool @private { get; set; }
                public WorkFlow_Owner owner { get; set; }
                public string html_url { get; set; }
                public string description { get; set; }
                public bool fork { get; set; }
                public string url { get; set; }
                public string forks_url { get; set; }
                public string keys_url { get; set; }
                public string collaborators_url { get; set; }
                public string teams_url { get; set; }
                public string hooks_url { get; set; }
                public string issue_events_url { get; set; }
                public string events_url { get; set; }
                public string assignees_url { get; set; }
                public string branches_url { get; set; }
                public string tags_url { get; set; }
                public string blobs_url { get; set; }
                public string git_tags_url { get; set; }
                public string git_refs_url { get; set; }
                public string trees_url { get; set; }
                public string statuses_url { get; set; }
                public string languages_url { get; set; }
                public string stargazers_url { get; set; }
                public string contributors_url { get; set; }
                public string subscribers_url { get; set; }
                public string subscription_url { get; set; }
                public string commits_url { get; set; }
                public string git_commits_url { get; set; }
                public string comments_url { get; set; }
                public string issue_comment_url { get; set; }
                public string contents_url { get; set; }
                public string compare_url { get; set; }
                public string merges_url { get; set; }
                public string archive_url { get; set; }
                public string downloads_url { get; set; }
                public string issues_url { get; set; }
                public string pulls_url { get; set; }
                public string milestones_url { get; set; }
                public string notifications_url { get; set; }
                public string labels_url { get; set; }
                public string releases_url { get; set; }
                public string deployments_url { get; set; }
            }

            public class WorkFlow_Owner
            {
                public string login { get; set; }
                public int id { get; set; }
                public string node_id { get; set; }
                public string avatar_url { get; set; }
                public string gravatar_id { get; set; }
                public string url { get; set; }
                public string html_url { get; set; }
                public string followers_url { get; set; }
                public string following_url { get; set; }
                public string gists_url { get; set; }
                public string starred_url { get; set; }
                public string subscriptions_url { get; set; }
                public string organizations_url { get; set; }
                public string repos_url { get; set; }
                public string events_url { get; set; }
                public string received_events_url { get; set; }
                public string type { get; set; }
                public bool site_admin { get; set; }
            }

            public class WorkFlow_Repository
            {
                public int id { get; set; }
                public string node_id { get; set; }
                public string name { get; set; }
                public string full_name { get; set; }
                public bool @private { get; set; }
                public WorkFlow_Owner owner { get; set; }
                public string html_url { get; set; }
                public string description { get; set; }
                public bool fork { get; set; }
                public string url { get; set; }
                public string forks_url { get; set; }
                public string keys_url { get; set; }
                public string collaborators_url { get; set; }
                public string teams_url { get; set; }
                public string hooks_url { get; set; }
                public string issue_events_url { get; set; }
                public string events_url { get; set; }
                public string assignees_url { get; set; }
                public string branches_url { get; set; }
                public string tags_url { get; set; }
                public string blobs_url { get; set; }
                public string git_tags_url { get; set; }
                public string git_refs_url { get; set; }
                public string trees_url { get; set; }
                public string statuses_url { get; set; }
                public string languages_url { get; set; }
                public string stargazers_url { get; set; }
                public string contributors_url { get; set; }
                public string subscribers_url { get; set; }
                public string subscription_url { get; set; }
                public string commits_url { get; set; }
                public string git_commits_url { get; set; }
                public string comments_url { get; set; }
                public string issue_comment_url { get; set; }
                public string contents_url { get; set; }
                public string compare_url { get; set; }
                public string merges_url { get; set; }
                public string archive_url { get; set; }
                public string downloads_url { get; set; }
                public string issues_url { get; set; }
                public string pulls_url { get; set; }
                public string milestones_url { get; set; }
                public string notifications_url { get; set; }
                public string labels_url { get; set; }
                public string releases_url { get; set; }
                public string deployments_url { get; set; }
            }

            public class WorkFlow_Root
            {
                public int total_count { get; set; }
                public List<WorkflowRun> workflow_runs { get; set; }
            }

            public class WorkFlow_TriggeringActor
            {
                public string login { get; set; }
                public int id { get; set; }
                public string node_id { get; set; }
                public string avatar_url { get; set; }
                public string gravatar_id { get; set; }
                public string url { get; set; }
                public string html_url { get; set; }
                public string followers_url { get; set; }
                public string following_url { get; set; }
                public string gists_url { get; set; }
                public string starred_url { get; set; }
                public string subscriptions_url { get; set; }
                public string organizations_url { get; set; }
                public string repos_url { get; set; }
                public string events_url { get; set; }
                public string received_events_url { get; set; }
                public string type { get; set; }
                public bool site_admin { get; set; }
            }

            public class WorkflowRun
            {
                public object id { get; set; }
                public string name { get; set; }
                public string node_id { get; set; }
                public string head_branch { get; set; }
                public string head_sha { get; set; }
                public string path { get; set; }
                public string display_title { get; set; }
                public int run_number { get; set; }
                public string @event { get; set; }
                public string status { get; set; }
                public string conclusion { get; set; }
                public int workflow_id { get; set; }
                public object check_suite_id { get; set; }
                public string check_suite_node_id { get; set; }
                public string url { get; set; }
                public string html_url { get; set; }
                public List<object> pull_requests { get; set; }
                public DateTime created_at { get; set; }
                public DateTime updated_at { get; set; }
                public WorkFlow_Actor actor { get; set; }
                public int run_attempt { get; set; }
                public List<object> referenced_workflows { get; set; }
                public DateTime run_started_at { get; set; }
                public WorkFlow_TriggeringActor triggering_actor { get; set; }
                public string jobs_url { get; set; }
                public string logs_url { get; set; }
                public string check_suite_url { get; set; }
                public string artifacts_url { get; set; }
                public string cancel_url { get; set; }
                public string rerun_url { get; set; }
                public string previous_attempt_url { get; set; }
                public string workflow_url { get; set; }
                public WorkFlow_HeadCommit head_commit { get; set; }
                public WorkFlow_Repository repository { get; set; }
                public WorkFlow_HeadRepository head_repository { get; set; }
            }
        }

        public class fpPS4_Artifact_Json
        {
            public class FP_Artifact
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
                public FP_WorkflowRun workflow_run { get; set; }
            }

            public class FP_WorkflowRun
            {
                public object id { get; set; }
                public int repository_id { get; set; }
                public int head_repository_id { get; set; }
                public string head_branch { get; set; }
                public string head_sha { get; set; }
            }

            public class FP_Root
            {
                public int total_count { get; set; }
                public List<FP_Artifact> artifacts { get; set; }
            }
        }
    }
}
