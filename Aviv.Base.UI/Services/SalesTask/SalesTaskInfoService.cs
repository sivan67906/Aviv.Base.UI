using Aviv.Base.UI.Models.SalesTask;

namespace Aviv.Base.UI.Services.SalesTask
{
    public class SalesTaskInfoService
    {
        private readonly List<ST_MasterViewModel> _salesTasks = [];
        private readonly List<ST_AssignmentLogViewModel> _assignmentLogs = [];
        private readonly List<ST_StatusTimelineViewModel> _statusTimelines = [];
        private readonly List<ST_LogViewModel> _logs = [];
        private readonly List<ST_FollowUpTrackerViewModel> _followUps = [];
        private readonly List<ST_MeetingSchedulerViewModel> _meetings = [];
        private readonly List<ST_ReminderViewModel> _reminders = [];
        private readonly List<ST_OutcomeLogViewModel> _outcomeLogs = [];
        private readonly List<ST_ActivityAuditTrailViewModel> _activityAuditTrails = [];
        private readonly List<ST_SalesRepProductivityMetricsViewModel> _salesRepProductivityMetrics = [];

        // Mock user data
        private readonly Dictionary<Guid, string> _users = [];
        private readonly Dictionary<Guid, string> _roles = [];
        private readonly Dictionary<Guid, string> _campaigns = [];
        private readonly Dictionary<Guid, string> _products = [];
        private readonly Dictionary<Guid, string> _customers = [];
        private readonly Dictionary<Guid, string> _salesTeams = [];
        private readonly Dictionary<Guid, string> _opportunities = [];

        // Reference Data Lists
        private readonly List<string> _taskTypes = ["Call", "Email", "Meeting", "Demo", "Site Visit", "Proposal", "Follow-up", "Document Collection", "Internal Review", "Custom"];
        private readonly List<string> _taskPriorities = ["Low", "Medium", "High", "Critical"];
        private readonly List<string> _taskStatuses = ["Open", "In Progress", "Completed", "On Hold", "Cancelled", "Escalated"];
        private readonly List<string> _associatedModules = ["Lead", "Opportunity", "Customer", "Internal", "Campaign"];
        private readonly List<string> _sourceChannels = ["Manual", "Workflow Automation", "Campaign Trigger", "API", "Mobile App", "Reminder Engine"];
        private readonly List<string> _customerVisibilityLevels = ["Internal Only", "Customer Visible", "Manager Only"];
        private readonly List<string> _actionTypes = ["Assigned", "Reassigned", "Delegated", "AutoAssigned", "Escalated", "Completed", "Cancelled", "MarkedUrgent"];
        private readonly List<string> _assignmentChannels = ["Manual", "WorkflowRule", "AutoEscalation", "RoundRobin", "SkillBasedRouting"];
        private readonly List<string> _taskPreviousStatuses = ["New", "Assigned", "InProgress", "OnHold", "Completed", "Cancelled", "Deferred", "Reopened"];
        private readonly List<string> _statusChangeChannels = ["Manual", "WorkflowRule", "AutoEscalation", "TaskSLAEngine", "MobileApp", "WebPortal", "API"];
        private readonly List<string> _activityTypes = ["Call", "Meeting", "Note", "Email", "SMS", "FileUpload", "WhatsApp", "FollowUp", "Reminder", "SystemUpdate", "Reschedule", "EscalationNote"];
        private readonly List<string> _activityChannels = ["ManualEntry", "MobileApp", "WebApp", "API", "SystemGenerated", "EmailParser", "AutoSync"];
        private readonly List<string> _relatedEntityTypes = ["Lead", "Opportunity", "Customer", "Account", "Contact", "SalesTask", "Campaign"];
        private readonly List<string> _followUpStatuses = ["Pending", "Completed", "Rescheduled", "Missed", "Cancelled", "Delegated"];
        private readonly List<string> _hostTypes = ["Internal", "External", "Joint"];
        private readonly List<string> _meetingModes = ["InPerson", "Online", "Hybrid"];
        private readonly List<string> _meetingTypes = ["Demo", "Negotiation", "Pitch", "Discovery", "Training", "Internal Sync"];
        private readonly List<string> _reminderTypes = ["Task", "Meeting", "FollowUp", "Engagement", "Custom"];
        private readonly List<string> _reminderSources = ["Manual", "SystemGenerated", "Workflow", "Recurring"];
        private readonly List<string> _repeatIntervalTypes = ["Hourly", "Daily", "Weekly"];
        private readonly List<string> _reminderStatuses = ["Scheduled", "Sent", "Acknowledged", "Failed", "Cancelled"];
        private readonly List<string> _outcomeTypes = ["CallAnswered", "CallMissed", "MeetingHeld", "Rescheduled", "NoResponse", "DealAdvanced", "Objection", "NotInterested"];
        private readonly List<string> _outcomeStatuses = ["Success", "Partial", "Failed", "PendingFollowUp", "Deferred", "Escalated"];
        private readonly List<string> _customerReactions = ["Positive", "Neutral", "Negative", "Objection", "Unresponsive"];
        private readonly List<string> _salesRepSentiments = ["Confident", "Uncertain", "Need Help", "Success", "Worried"];
        private readonly List<string> _changeTypes = ["Created", "Assigned", "Updated", "StatusChanged", "OutcomeLogged", "FollowUpScheduled", "Reassigned", "ReminderSet", "Deleted"];
        private readonly List<string> _actionSources = ["WebApp", "MobileApp", "API", "System", "WorkflowEngine"];
        private readonly List<string> _evaluationPeriods = ["Daily", "Weekly", "Monthly", "Quarterly"];
        private readonly List<string> _recurrencePatterns = ["Daily", "Weekly", "Monthly", "Custom"];

        public SalesTaskInfoService()
        {
            // Initialize mock data
            InitializeMockUsers();
            InitializeMockRoles();
            InitializeMockCampaigns();
            InitializeMockProducts();
            InitializeMockCustomers();
            InitializeMockSalesTeams();
            InitializeMockOpportunities();

            // Initialize sample data for all entities
            InitializeSampleSalesTasks();
            InitializeSampleAssignmentLogs();
            InitializeSampleStatusTimelines();
            InitializeSampleLogs();
            InitializeSampleFollowUps();
            InitializeSampleMeetings();
            InitializeSampleReminders();
            InitializeSampleOutcomeLogs();
            InitializeSampleActivityAuditTrails();
            InitializeSampleSalesRepProductivityMetrics();
        }

        #region Master Sales Task Methods

        public async Task<List<ST_MasterViewModel>> GetSalesTasksAsync(int skip = 0, int take = 10, string searchText = "")
        {
            await Task.Delay(300); // Simulate network delay

            IQueryable<ST_MasterViewModel> query = _salesTasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.TaskCode.ToLower().Contains(searchText) ||
                    c.TaskTitle.ToLower().Contains(searchText) ||
                    c.TaskType.ToLower().Contains(searchText) ||
                    c.TaskStatus.ToLower().Contains(searchText));
            }

            return query.OrderByDescending(c => c.CreatedDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetSalesTasksCountAsync(string searchText = "")
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_MasterViewModel> query = _salesTasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.TaskCode.ToLower().Contains(searchText) ||
                    c.TaskTitle.ToLower().Contains(searchText) ||
                    c.TaskType.ToLower().Contains(searchText) ||
                    c.TaskStatus.ToLower().Contains(searchText));
            }

            return query.Count();
        }

        public async Task<ST_MasterViewModel> GetSalesTaskByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _salesTasks.FirstOrDefault(c => c.TaskId == id);
        }

        public async Task<SalesTaskInfoViewModel> GetFullSalesTaskByIdAsync(Guid id)
        {
            await Task.Delay(300); // Simulate network delay

            ST_MasterViewModel? master = _salesTasks.FirstOrDefault(c => c.TaskId == id);

            if (master == null)
            {
                return null;
            }

            SalesTaskInfoViewModel result = new SalesTaskInfoViewModel
            {
                Master = master,
                AssignmentLogs = _assignmentLogs.Where(c => c.SalesTaskId == id).ToList(),
                StatusTimelines = _statusTimelines.Where(c => c.SalesTaskId == id).ToList(),
                Logs = _logs.Where(c => c.SalesTaskId == id).ToList(),
                FollowUps = _followUps.Where(c => c.SalesTaskId == id).ToList(),
                Meetings = _meetings.Where(c => c.RelatedEntityType == "SalesTask" && c.RelatedEntityId == id).ToList(),
                Reminders = _reminders.Where(c => c.RelatedEntityType == "SalesTask" && c.RelatedEntityId == id).ToList(),
                OutcomeLogs = _outcomeLogs.Where(c => c.SalesTaskId == id).ToList(),
                ActivityAuditTrails = _activityAuditTrails.Where(c => c.SalesActivityId == id).ToList(),
                ProductivityMetrics = _salesRepProductivityMetrics.Where(c => c.SalesRepUserId == master.AssignedToUserId).ToList()
            };

            return result;
        }

        public async Task<ST_MasterViewModel> CreateSalesTaskAsync(ST_MasterViewModel model)
        {
            await Task.Delay(400); // Simulate network delay

            model.TaskId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user
            model.TaskCode = GenerateTaskCode();

            // Add display names from IDs
            model.CreatedByUserName = GetUserName(model.CreatedByUserId);
            model.AssignedToUserName = GetUserName(model.AssignedToUserId);

            if (model.AssignedToRoleId.HasValue)
            {
                model.AssignedToRoleName = GetRoleName(model.AssignedToRoleId.Value);
            }

            if (model.DelegatedToUserId.HasValue)
            {
                model.DelegatedToUserName = GetUserName(model.DelegatedToUserId.Value);
            }

            if (model.EscalatedToUserId.HasValue)
            {
                model.EscalatedToUserName = GetUserName(model.EscalatedToUserId.Value);
            }

            if (model.RelatedCampaignId.HasValue)
            {
                model.RelatedCampaignName = GetCampaignName(model.RelatedCampaignId.Value);
            }

            if (model.RelatedProductId.HasValue)
            {
                model.RelatedProductName = GetProductName(model.RelatedProductId.Value);
            }

            _salesTasks.Add(model);

            // Add initial status timeline entry
            ST_StatusTimelineViewModel statusTimeline = new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.NewGuid(),
                SalesTaskId = model.TaskId,
                PreviousStatus = "New",
                NewStatus = model.TaskStatus,
                StatusChangedByUserId = model.CreatedByUserId,
                StatusChangedByUserName = GetUserName(model.CreatedByUserId),
                StatusChangedOn = DateTime.Now,
                StatusChangeNote = "Initial task creation",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                IsFinalStatus = false,
                TotalDurationFromStart = TimeSpan.Zero,
                CreatedDate = DateTime.Now,
                CreatedBy = "System"
            };

            _statusTimelines.Add(statusTimeline);

            // Add initial assignment log
            ST_AssignmentLogViewModel assignmentLog = new ST_AssignmentLogViewModel
            {
                LogId = Guid.NewGuid(),
                SalesTaskId = model.TaskId,
                ActionType = "Assigned",
                ActionTimestamp = DateTime.Now,
                ToUserId = model.AssignedToUserId,
                ToUserName = GetUserName(model.AssignedToUserId),
                IsSystemGenerated = false,
                PerformedByUserId = model.CreatedByUserId,
                PerformedByUserName = GetUserName(model.CreatedByUserId),
                AssignmentNote = "Initial task assignment",
                TaskPriorityAtAssignment = model.TaskPriority,
                DueDateAtAssignment = model.DueDateTime,
                IsNotified = true,
                NotificationTimestamp = DateTime.Now,
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now,
                CreatedBy = "System"
            };

            _assignmentLogs.Add(assignmentLog);

            // Add initial activity audit trail
            ST_ActivityAuditTrailViewModel activityAuditTrail = new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.NewGuid(),
                SalesActivityId = model.TaskId,
                ChangeType = "Created",
                ActionPerformedByUserId = model.CreatedByUserId,
                ActionPerformedByUserName = GetUserName(model.CreatedByUserId),
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now,
                ActionSource = "WebApp",
                NewStatus = model.TaskStatus,
                NewAssigneeId = model.AssignedToUserId,
                NewAssigneeName = GetUserName(model.AssignedToUserId),
                NewDueDate = model.DueDateTime,
                ChangeSummary = "Task created",
                RequiresFollowUp = false,
                CreatedDate = DateTime.Now,
                CreatedBy = "System"
            };

            _activityAuditTrails.Add(activityAuditTrail);

            return model;
        }

        public async Task<ST_MasterViewModel> UpdateSalesTaskAsync(ST_MasterViewModel model)
        {
            await Task.Delay(400); // Simulate network delay

            ST_MasterViewModel? existingTask = _salesTasks.FirstOrDefault(c => c.TaskId == model.TaskId);

            if (existingTask == null)
            {
                throw new Exception($"Sales Task with ID {model.TaskId} not found");
            }

            // Track changes for audit trail
            string previousStatus = existingTask.TaskStatus;
            Guid previousAssigneeId = existingTask.AssignedToUserId;
            DateTime? previousDueDate = existingTask.DueDateTime;

            // Update properties
            existingTask.TaskTitle = model.TaskTitle;
            existingTask.TaskDescription = model.TaskDescription;
            existingTask.TaskType = model.TaskType;
            existingTask.TaskPriority = model.TaskPriority;
            existingTask.DueDateTime = model.DueDateTime;
            existingTask.StartDateTime = model.StartDateTime;
            existingTask.TaskStatus = model.TaskStatus;
            existingTask.AssociatedModule = model.AssociatedModule;
            existingTask.AssociatedRecordId = model.AssociatedRecordId;
            existingTask.AssignedToUserId = model.AssignedToUserId;
            existingTask.AssignedToUserName = GetUserName(model.AssignedToUserId);
            existingTask.AssignedToRoleId = model.AssignedToRoleId;

            if (model.AssignedToRoleId.HasValue)
            {
                existingTask.AssignedToRoleName = GetRoleName(model.AssignedToRoleId.Value);
            }
            else
            {
                existingTask.AssignedToRoleName = null;
            }

            existingTask.DelegatedToUserId = model.DelegatedToUserId;

            if (model.DelegatedToUserId.HasValue)
            {
                existingTask.DelegatedToUserName = GetUserName(model.DelegatedToUserId.Value);
            }
            else
            {
                existingTask.DelegatedToUserName = null;
            }

            existingTask.EscalatedToUserId = model.EscalatedToUserId;

            if (model.EscalatedToUserId.HasValue)
            {
                existingTask.EscalatedToUserName = GetUserName(model.EscalatedToUserId.Value);
            }
            else
            {
                existingTask.EscalatedToUserName = null;
            }

            existingTask.IsRecurringTask = model.IsRecurringTask;
            existingTask.RecurrencePattern = model.RecurrencePattern;
            existingTask.TaskTags = model.TaskTags;
            existingTask.RelatedCampaignId = model.RelatedCampaignId;

            if (model.RelatedCampaignId.HasValue)
            {
                existingTask.RelatedCampaignName = GetCampaignName(model.RelatedCampaignId.Value);
            }
            else
            {
                existingTask.RelatedCampaignName = null;
            }

            existingTask.RelatedProductId = model.RelatedProductId;

            if (model.RelatedProductId.HasValue)
            {
                existingTask.RelatedProductName = GetProductName(model.RelatedProductId.Value);
            }
            else
            {
                existingTask.RelatedProductName = null;
            }

            existingTask.SourceChannel = model.SourceChannel;
            existingTask.ExpectedOutcome = model.ExpectedOutcome;
            existingTask.HasAttachment = model.HasAttachment;
            existingTask.IsClientFacing = model.IsClientFacing;
            existingTask.CustomerVisibilityLevel = model.CustomerVisibilityLevel;
            existingTask.CustomFieldsJson = model.CustomFieldsJson;
            existingTask.LastModifiedDate = DateTime.Now;
            existingTask.LastModifiedBy = "System"; // In a real app, this would be the logged-in user
            existingTask.UploadedDocuments = model.UploadedDocuments;

            // Add status timeline entry if status changed
            if (previousStatus != model.TaskStatus)
            {
                ST_StatusTimelineViewModel statusTimeline = new ST_StatusTimelineViewModel
                {
                    StatusTimelineId = Guid.NewGuid(),
                    SalesTaskId = model.TaskId,
                    PreviousStatus = previousStatus,
                    NewStatus = model.TaskStatus,
                    StatusChangedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Sample user ID (in real app, this would be the current user)
                    StatusChangedByUserName = "System User", // In real app, this would be the current user's name
                    StatusChangedOn = DateTime.Now,
                    StatusChangeNote = "Task status updated",
                    StatusChangeChannel = "Manual",
                    IsSystemGenerated = false,
                    IsFinalStatus = model.TaskStatus == "Completed" || model.TaskStatus == "Cancelled",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System"
                };

                // Calculate durations
                ST_StatusTimelineViewModel? previousStatusChange = _statusTimelines
                    .Where(st => st.SalesTaskId == model.TaskId)
                    .OrderByDescending(st => st.StatusChangedOn)
                    .FirstOrDefault();

                if (previousStatusChange != null)
                {
                    statusTimeline.DurationFromPreviousStatus = DateTime.Now - previousStatusChange.StatusChangedOn;
                }

                statusTimeline.TotalDurationFromStart = DateTime.Now - existingTask.CreatedDate;

                _statusTimelines.Add(statusTimeline);
            }

            // Add assignment log if assignee changed
            if (previousAssigneeId != model.AssignedToUserId)
            {
                ST_AssignmentLogViewModel assignmentLog = new ST_AssignmentLogViewModel
                {
                    LogId = Guid.NewGuid(),
                    SalesTaskId = model.TaskId,
                    ActionType = "Reassigned",
                    ActionTimestamp = DateTime.Now,
                    FromUserId = previousAssigneeId,
                    FromUserName = GetUserName(previousAssigneeId),
                    ToUserId = model.AssignedToUserId,
                    ToUserName = GetUserName(model.AssignedToUserId),
                    IsSystemGenerated = false,
                    PerformedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Sample user ID (in real app, this would be the current user)
                    PerformedByUserName = "System User", // In real app, this would be the current user's name
                    AssignmentNote = "Task reassigned",
                    TaskPriorityAtAssignment = model.TaskPriority,
                    DueDateAtAssignment = model.DueDateTime,
                    IsNotified = true,
                    NotificationTimestamp = DateTime.Now,
                    AssignmentChannel = "Manual",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System"
                };

                _assignmentLogs.Add(assignmentLog);
            }

            // Add activity audit trail
            ST_ActivityAuditTrailViewModel activityAuditTrail = new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.NewGuid(),
                SalesActivityId = model.TaskId,
                ChangeType = "Updated",
                ActionPerformedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Sample user ID (in real app, this would be the current user)
                ActionPerformedByUserName = "System User", // In real app, this would be the current user's name
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now,
                ActionSource = "WebApp",
                CreatedDate = DateTime.Now,
                CreatedBy = "System"
            };

            // Add change tracking info if relevant
            if (previousStatus != model.TaskStatus)
            {
                activityAuditTrail.PreviousStatus = previousStatus;
                activityAuditTrail.NewStatus = model.TaskStatus;
            }

            if (previousAssigneeId != model.AssignedToUserId)
            {
                activityAuditTrail.PreviousAssigneeId = previousAssigneeId;
                activityAuditTrail.PreviousAssigneeName = GetUserName(previousAssigneeId);
                activityAuditTrail.NewAssigneeId = model.AssignedToUserId;
                activityAuditTrail.NewAssigneeName = GetUserName(model.AssignedToUserId);
            }

            if (previousDueDate != model.DueDateTime)
            {
                activityAuditTrail.PreviousDueDate = previousDueDate;
                activityAuditTrail.NewDueDate = model.DueDateTime;
            }

            activityAuditTrail.ChangeSummary = "Task updated";

            _activityAuditTrails.Add(activityAuditTrail);

            return existingTask;
        }

        public async Task DeleteSalesTaskAsync(Guid id)
        {
            await Task.Delay(300); // Simulate network delay

            ST_MasterViewModel? task = _salesTasks.FirstOrDefault(c => c.TaskId == id);

            if (task == null)
            {
                throw new Exception($"Sales Task with ID {id} not found");
            }

            // Add activity audit trail for deletion
            ST_ActivityAuditTrailViewModel activityAuditTrail = new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.NewGuid(),
                SalesActivityId = id,
                ChangeType = "Deleted",
                ActionPerformedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Sample user ID (in real app, this would be the current user)
                ActionPerformedByUserName = "System User", // In real app, this would be the current user's name
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now,
                ActionSource = "WebApp",
                PreviousStatus = task.TaskStatus,
                ChangeSummary = "Task deleted",
                CreatedDate = DateTime.Now,
                CreatedBy = "System"
            };

            _activityAuditTrails.Add(activityAuditTrail);

            _salesTasks.Remove(task);
        }

        public List<string> GetTaskTypes() => _taskTypes;
        public List<string> GetTaskPriorities() => _taskPriorities;
        public List<string> GetTaskStatuses() => _taskStatuses;
        public List<string> GetAssociatedModules() => _associatedModules;
        public List<string> GetSourceChannels() => _sourceChannels;
        public List<string> GetCustomerVisibilityLevels() => _customerVisibilityLevels;
        public List<KeyValuePair<Guid, string>> GetUsers() => _users.Select(u => new KeyValuePair<Guid, string>(u.Key, u.Value)).ToList();
        public List<KeyValuePair<Guid, string>> GetRoles() => _roles.Select(r => new KeyValuePair<Guid, string>(r.Key, r.Value)).ToList();
        public List<KeyValuePair<Guid, string>> GetCampaigns() => _campaigns.Select(c => new KeyValuePair<Guid, string>(c.Key, c.Value)).ToList();
        public List<KeyValuePair<Guid, string>> GetProducts() => _products.Select(p => new KeyValuePair<Guid, string>(p.Key, p.Value)).ToList();
        public List<KeyValuePair<Guid, string>> GetCustomers() => _customers.Select(c => new KeyValuePair<Guid, string>(c.Key, c.Value)).ToList();
        public List<KeyValuePair<Guid, string>> GetSalesTeams() => _salesTeams.Select(t => new KeyValuePair<Guid, string>(t.Key, t.Value)).ToList();
        private string GenerateTaskCode()
        {
            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            int count = _salesTasks.Count + 1;
            return $"TASK-{dateStr}-{count:D3}";
        }

        #endregion

        #region Assignment Log Methods

        public async Task<List<ST_AssignmentLogViewModel>> GetAssignmentLogsAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_AssignmentLogViewModel> query = _assignmentLogs.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ActionType!.ToLower().Contains(searchText) ||
                    (c.FromUserName != null && c.FromUserName.ToLower().Contains(searchText)) ||
                    c.ToUserName!.ToLower().Contains(searchText) ||
                    (c.AssignmentNote != null && c.AssignmentNote.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.ActionTimestamp)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetAssignmentLogsCountAsync(string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_AssignmentLogViewModel> query = _assignmentLogs.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }


            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ActionType!.ToLower().Contains(searchText) ||
                    (c.FromUserName != null && c.FromUserName.ToLower().Contains(searchText)) ||
                    c.ToUserName!.ToLower().Contains(searchText) ||
                    (c.AssignmentNote != null && c.AssignmentNote.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_AssignmentLogViewModel> GetAssignmentLogByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _assignmentLogs.FirstOrDefault(c => c.LogId == id);
        }

        public async Task<ST_AssignmentLogViewModel> CreateAssignmentLogAsync(ST_AssignmentLogViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.LogId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.ToUserName = GetUserName(model.ToUserId);

            if (model.FromUserId.HasValue)
            {
                model.FromUserName = GetUserName(model.FromUserId.Value);
            }

            model.PerformedByUserName = GetUserName(model.PerformedByUserId);

            _assignmentLogs.Add(model);

            return model;
        }

        public async Task<ST_AssignmentLogViewModel> UpdateAssignmentLogAsync(ST_AssignmentLogViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_AssignmentLogViewModel? existingLog = _assignmentLogs.FirstOrDefault(c => c.LogId == model.LogId);

            if (existingLog == null)
            {
                throw new Exception($"Assignment Log with ID {model.LogId} not found");
            }

            // Update properties
            existingLog.ActionType = model.ActionType;
            existingLog.ActionTimestamp = model.ActionTimestamp;
            existingLog.FromUserId = model.FromUserId;

            if (model.FromUserId.HasValue)
            {
                existingLog.FromUserName = GetUserName(model.FromUserId.Value);
            }
            else
            {
                existingLog.FromUserName = null;
            }

            existingLog.ToUserId = model.ToUserId;
            existingLog.ToUserName = GetUserName(model.ToUserId);
            existingLog.IsSystemGenerated = model.IsSystemGenerated;
            existingLog.PerformedByUserId = model.PerformedByUserId;
            existingLog.PerformedByUserName = GetUserName(model.PerformedByUserId);
            existingLog.AssignmentNote = model.AssignmentNote;
            existingLog.TaskPriorityAtAssignment = model.TaskPriorityAtAssignment;
            existingLog.DueDateAtAssignment = model.DueDateAtAssignment;
            existingLog.EscalationLevel = model.EscalationLevel;
            existingLog.IsNotified = model.IsNotified;
            existingLog.NotificationTimestamp = model.NotificationTimestamp;
            existingLog.AssignmentChannel = model.AssignmentChannel;
            existingLog.TriggerSource = model.TriggerSource;
            existingLog.IpAddress = model.IpAddress;
            existingLog.UserAgent = model.UserAgent;

            return existingLog;
        }

        public async Task DeleteAssignmentLogAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_AssignmentLogViewModel? log = _assignmentLogs.FirstOrDefault(c => c.LogId == id);

            if (log == null)
            {
                throw new Exception($"Assignment Log with ID {id} not found");
            }

            _assignmentLogs.Remove(log);
        }

        public List<string> GetActionTypes() => _actionTypes;
        public List<string> GetAssignmentChannels() => _assignmentChannels;

        #endregion

        #region Status Timeline Methods

        public async Task<List<ST_StatusTimelineViewModel>> GetStatusTimelinesAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_StatusTimelineViewModel> query = _statusTimelines.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.PreviousStatus!.ToLower().Contains(searchText) ||
                    c.NewStatus!.ToLower().Contains(searchText) ||
                    c.StatusChangedByUserName!.ToLower().Contains(searchText) ||
                    (c.StatusChangeNote != null && c.StatusChangeNote.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.StatusChangedOn)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetStatusTimelinesCountAsync(string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_StatusTimelineViewModel> query = _statusTimelines.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.PreviousStatus.ToLower().Contains(searchText) ||
                    c.NewStatus.ToLower().Contains(searchText) ||
                    c.StatusChangedByUserName.ToLower().Contains(searchText) ||
                    (c.StatusChangeNote != null && c.StatusChangeNote.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_StatusTimelineViewModel> GetStatusTimelineByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _statusTimelines.FirstOrDefault(c => c.StatusTimelineId == id);
        }

        public async Task<ST_StatusTimelineViewModel> CreateStatusTimelineAsync(ST_StatusTimelineViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.StatusTimelineId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.StatusChangedByUserName = GetUserName(model.StatusChangedByUserId);

            _statusTimelines.Add(model);

            return model;
        }

        public async Task<ST_StatusTimelineViewModel> UpdateStatusTimelineAsync(ST_StatusTimelineViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_StatusTimelineViewModel? existingTimeline = _statusTimelines.FirstOrDefault(c => c.StatusTimelineId == model.StatusTimelineId);

            if (existingTimeline == null)
            {
                throw new Exception($"Status Timeline with ID {model.StatusTimelineId} not found");
            }

            // Update properties
            existingTimeline.PreviousStatus = model.PreviousStatus;
            existingTimeline.NewStatus = model.NewStatus;
            existingTimeline.StatusChangedByUserId = model.StatusChangedByUserId;
            existingTimeline.StatusChangedByUserName = GetUserName(model.StatusChangedByUserId);
            existingTimeline.StatusChangedOn = model.StatusChangedOn;
            existingTimeline.StatusChangeNote = model.StatusChangeNote;
            existingTimeline.StatusChangeChannel = model.StatusChangeChannel;
            existingTimeline.IsSystemGenerated = model.IsSystemGenerated;
            existingTimeline.RelatedActionId = model.RelatedActionId;
            existingTimeline.TriggerSource = model.TriggerSource;
            existingTimeline.IsFinalStatus = model.IsFinalStatus;
            existingTimeline.DurationFromPreviousStatus = model.DurationFromPreviousStatus;
            existingTimeline.TotalDurationFromStart = model.TotalDurationFromStart;
            existingTimeline.IpAddress = model.IpAddress;
            existingTimeline.UserAgent = model.UserAgent;

            return existingTimeline;
        }

        public async Task DeleteStatusTimelineAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_StatusTimelineViewModel? timeline = _statusTimelines.FirstOrDefault(c => c.StatusTimelineId == id);

            if (timeline == null)
            {
                throw new Exception($"Status Timeline with ID {id} not found");
            }

            _statusTimelines.Remove(timeline);
        }

        public List<string> GetTaskPreviousStatuses() => _taskPreviousStatuses;
        public List<string> GetStatusChangeChannels() => _statusChangeChannels;

        #endregion

        #region Log Methods

        public async Task<List<ST_LogViewModel>> GetLogsAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_LogViewModel> query = _logs.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ActivityType.ToLower().Contains(searchText) ||
                    c.PerformedByUserName.ToLower().Contains(searchText) ||
                    c.ActivityDescription.ToLower().Contains(searchText) ||
                    (c.ActivityResult != null && c.ActivityResult.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.PerformedOn)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetLogsCountAsync(string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_LogViewModel> query = _logs.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ActivityType.ToLower().Contains(searchText) ||
                    c.PerformedByUserName.ToLower().Contains(searchText) ||
                    c.ActivityDescription.ToLower().Contains(searchText) ||
                    (c.ActivityResult != null && c.ActivityResult.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_LogViewModel> GetLogByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _logs.FirstOrDefault(c => c.ActivityLogId == id);
        }

        public async Task<ST_LogViewModel> CreateLogAsync(ST_LogViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.ActivityLogId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.PerformedByUserName = GetUserName(model.PerformedByUserId);

            _logs.Add(model);

            return model;
        }

        public async Task<ST_LogViewModel> UpdateLogAsync(ST_LogViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_LogViewModel? existingLog = _logs.FirstOrDefault(c => c.ActivityLogId == model.ActivityLogId);

            if (existingLog == null)
            {
                throw new Exception($"Log with ID {model.ActivityLogId} not found");
            }

            // Update properties
            existingLog.PerformedByUserId = model.PerformedByUserId;
            existingLog.PerformedByUserName = GetUserName(model.PerformedByUserId);
            existingLog.PerformedOn = model.PerformedOn;
            existingLog.ActivityType = model.ActivityType;
            existingLog.ActivityChannel = model.ActivityChannel;
            existingLog.ActivityDescription = model.ActivityDescription;
            existingLog.NextActionDate = model.NextActionDate;
            existingLog.ActivityResult = model.ActivityResult;
            existingLog.IsCustomerVisible = model.IsCustomerVisible;
            existingLog.IsImportantFlagged = model.IsImportantFlagged;
            existingLog.AttachmentUrl = model.AttachmentUrl;
            existingLog.ExternalMeetingLink = model.ExternalMeetingLink;
            existingLog.ReferenceEmailId = model.ReferenceEmailId;
            existingLog.AssociatedDocumentId = model.AssociatedDocumentId;
            existingLog.UploadedDocuments = model.UploadedDocuments;

            return existingLog;
        }

        public async Task DeleteLogAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_LogViewModel? log = _logs.FirstOrDefault(c => c.ActivityLogId == id);

            if (log == null)
            {
                throw new Exception($"Log with ID {id} not found");
            }

            _logs.Remove(log);
        }

        public List<string> GetActivityTypes() => _activityTypes;
        public List<string> GetActivityChannels() => _activityChannels;

        #endregion

        #region Follow-Up Tracker Methods

        public async Task<List<ST_FollowUpTrackerViewModel>> GetFollowUpsAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_FollowUpTrackerViewModel> query = _followUps.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.FollowUpReason.ToLower().Contains(searchText) ||
                    c.AssignedToUserName.ToLower().Contains(searchText) ||
                    c.FollowUpStatus.ToLower().Contains(searchText) ||
                    (c.OutcomeNotes != null && c.OutcomeNotes.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.ScheduledDateTime)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetFollowUpsCountAsync(string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_FollowUpTrackerViewModel> query = _followUps.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.FollowUpReason.ToLower().Contains(searchText) ||
                    c.AssignedToUserName.ToLower().Contains(searchText) ||
                    c.FollowUpStatus.ToLower().Contains(searchText) ||
                    (c.OutcomeNotes != null && c.OutcomeNotes.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_FollowUpTrackerViewModel> GetFollowUpByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _followUps.FirstOrDefault(c => c.FollowUpId == id);
        }

        public async Task<ST_FollowUpTrackerViewModel> CreateFollowUpAsync(ST_FollowUpTrackerViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.FollowUpId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.RelatedEntityName = GetEntityName(model.RelatedEntityType, model.RelatedEntityId);
            model.AssignedToUserName = GetUserName(model.AssignedToUserId);
            model.AssignedByUserName = GetUserName(model.AssignedByUserId);

            // Set IsSelfAssigned if assigned to and by are the same
            model.IsSelfAssigned = model.AssignedByUserId == model.AssignedToUserId;

            _followUps.Add(model);

            return model;
        }

        public async Task<ST_FollowUpTrackerViewModel> UpdateFollowUpAsync(ST_FollowUpTrackerViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_FollowUpTrackerViewModel? existingFollowUp = _followUps.FirstOrDefault(c => c.FollowUpId == model.FollowUpId);

            if (existingFollowUp == null)
            {
                throw new Exception($"Follow-Up with ID {model.FollowUpId} not found");
            }

            // Update properties
            existingFollowUp.RelatedEntityType = model.RelatedEntityType;
            existingFollowUp.RelatedEntityId = model.RelatedEntityId;
            existingFollowUp.RelatedEntityName = GetEntityName(model.RelatedEntityType, model.RelatedEntityId);
            existingFollowUp.ScheduledDateTime = model.ScheduledDateTime;
            existingFollowUp.DueDateTime = model.DueDateTime;
            existingFollowUp.FollowUpReason = model.FollowUpReason;
            existingFollowUp.ReminderBeforeInMinutes = model.ReminderBeforeInMinutes;
            existingFollowUp.IsRecurring = model.IsRecurring;
            existingFollowUp.RecurrencePattern = model.RecurrencePattern;
            existingFollowUp.RecurrenceEndDate = model.RecurrenceEndDate;
            existingFollowUp.CustomRecurrenceDetails = model.CustomRecurrenceDetails;
            existingFollowUp.AssignedToUserId = model.AssignedToUserId;
            existingFollowUp.AssignedToUserName = GetUserName(model.AssignedToUserId);
            existingFollowUp.AssignedByUserId = model.AssignedByUserId;
            existingFollowUp.AssignedByUserName = GetUserName(model.AssignedByUserId);
            existingFollowUp.IsSelfAssigned = model.AssignedByUserId == model.AssignedToUserId;
            existingFollowUp.FollowUpStatus = model.FollowUpStatus;
            existingFollowUp.OutcomeNotes = model.OutcomeNotes;
            existingFollowUp.CustomerResponseSummary = model.CustomerResponseSummary;
            existingFollowUp.SendEmailReminder = model.SendEmailReminder;
            existingFollowUp.SendInAppNotification = model.SendInAppNotification;
            existingFollowUp.SendSMSReminder = model.SendSMSReminder;
            existingFollowUp.NotificationSent = model.NotificationSent;
            existingFollowUp.LastModifiedDate = DateTime.Now;
            existingFollowUp.LastModifiedBy = "System"; // In a real app, this would be the logged-in user

            return existingFollowUp;
        }

        public async Task DeleteFollowUpAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_FollowUpTrackerViewModel? followUp = _followUps.FirstOrDefault(c => c.FollowUpId == id);

            if (followUp == null)
            {
                throw new Exception($"Follow-Up with ID {id} not found");
            }

            _followUps.Remove(followUp);
        }

        public List<string> GetRelatedEntityTypes() => _relatedEntityTypes;
        public List<string> GetFollowUpStatuses() => _followUpStatuses;
        public List<string> GetRecurrencePatterns() => _recurrencePatterns;

        #endregion

        #region Meeting Scheduler Methods

        //public List<KeyValuePair<Guid, string>> GetOpportunities()
        //{
        //    var opportunities = new List<KeyValuePair<Guid, string>>();

        //    // Use consistent IDs with the sample opportunities from Edit.razor
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC01"), "MYSB Technologies Enterprise Solution"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC02"), "Petronas Digital Transformation Project"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC03"), "MalaysiaAir Cloud Migration"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC04"), "Bank Negara Security Infrastructure"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC05"), "KLCC Property Management System"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC06"), "Axiata 5G Implementation"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC07"), "Digi Communication Platform"));
        //    opportunities.Add(new KeyValuePair<Guid, string>(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), "Maybank Digital Banking Overhaul"));

        //    return opportunities;
        //}
        public async Task<List<ST_MeetingSchedulerViewModel>> GetMeetingsAsync(int skip = 0, int take = 10, string searchText = "", string? relatedEntityType = null, Guid? relatedEntityId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_MeetingSchedulerViewModel> query = _meetings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(relatedEntityType) && relatedEntityId.HasValue)
            {
                query = query.Where(c => c.RelatedEntityType == relatedEntityType && c.RelatedEntityId == relatedEntityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.MeetingTitle.ToLower().Contains(searchText) ||
                    c.OrganizerUserName.ToLower().Contains(searchText) ||
                    c.MeetingType.ToLower().Contains(searchText) ||
                    (c.MeetingAgenda != null && c.MeetingAgenda.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.MeetingDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetMeetingsCountAsync(string searchText = "", string? relatedEntityType = null, Guid? relatedEntityId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_MeetingSchedulerViewModel> query = _meetings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(relatedEntityType) && relatedEntityId.HasValue)
            {
                query = query.Where(c => c.RelatedEntityType == relatedEntityType && c.RelatedEntityId == relatedEntityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.MeetingTitle.ToLower().Contains(searchText) ||
                    c.OrganizerUserName.ToLower().Contains(searchText) ||
                    c.MeetingType.ToLower().Contains(searchText) ||
                    (c.MeetingAgenda != null && c.MeetingAgenda.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_MeetingSchedulerViewModel> GetMeetingByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _meetings.FirstOrDefault(c => c.MeetingId == id);
        }

        public async Task<ST_MeetingSchedulerViewModel> CreateMeetingAsync(ST_MeetingSchedulerViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.MeetingId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.OrganizerUserName = GetUserName(model.OrganizerUserId);

            if (model.RelatedEntityId.HasValue)
            {
                model.RelatedEntityName = GetEntityName(model.RelatedEntityType, model.RelatedEntityId.Value);
            }

            if (model.MeetingWithCustomerId.HasValue)
            {
                model.MeetingWithCustomerName = GetCustomerName(model.MeetingWithCustomerId.Value);
            }

            // Process participants
            model.ParticipantUserNames = [];

            foreach (Guid userId in model.ParticipantUserIds)
            {
                model.ParticipantUserNames.Add(GetUserName(userId));
            }

            _meetings.Add(model);

            return model;
        }

        public async Task<ST_MeetingSchedulerViewModel> UpdateMeetingAsync(ST_MeetingSchedulerViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_MeetingSchedulerViewModel? existingMeeting = _meetings.FirstOrDefault(c => c.MeetingId == model.MeetingId);

            if (existingMeeting == null)
            {
                throw new Exception($"Meeting with ID {model.MeetingId} not found");
            }

            // Update properties
            existingMeeting.MeetingTitle = model.MeetingTitle;
            existingMeeting.RelatedEntityType = model.RelatedEntityType;
            existingMeeting.RelatedEntityId = model.RelatedEntityId;

            if (model.RelatedEntityId.HasValue)
            {
                existingMeeting.RelatedEntityName = GetEntityName(model.RelatedEntityType, model.RelatedEntityId.Value);
            }
            else
            {
                existingMeeting.RelatedEntityName = null;
            }

            existingMeeting.MeetingDate = model.MeetingDate;
            existingMeeting.DurationInMinutes = model.DurationInMinutes;
            existingMeeting.Timezone = model.Timezone;
            existingMeeting.IsFullDayEvent = model.IsFullDayEvent;
            existingMeeting.OrganizerUserId = model.OrganizerUserId;
            existingMeeting.OrganizerUserName = GetUserName(model.OrganizerUserId);
            existingMeeting.ParticipantUserIds = model.ParticipantUserIds;

            // Process participants
            existingMeeting.ParticipantUserNames = [];
            foreach (Guid userId in model.ParticipantUserIds)
            {
                existingMeeting.ParticipantUserNames.Add(GetUserName(userId));
            }

            existingMeeting.ExternalParticipants = model.ExternalParticipants;
            existingMeeting.HostType = model.HostType;
            existingMeeting.MeetingWithCustomerId = model.MeetingWithCustomerId;

            if (model.MeetingWithCustomerId.HasValue)
            {
                existingMeeting.MeetingWithCustomerName = GetCustomerName(model.MeetingWithCustomerId.Value);
            }
            else
            {
                existingMeeting.MeetingWithCustomerName = null;
            }

            existingMeeting.MeetingMode = model.MeetingMode;
            existingMeeting.MeetingLocation = model.MeetingLocation;
            existingMeeting.MeetingLink = model.MeetingLink;
            existingMeeting.MapCoordinates = model.MapCoordinates;
            existingMeeting.MeetingAgenda = model.MeetingAgenda;
            existingMeeting.MeetingObjective = model.MeetingObjective;
            existingMeeting.MeetingType = model.MeetingType;
            existingMeeting.PreparationChecklist = model.PreparationChecklist;
            existingMeeting.IsRecurring = model.IsRecurring;
            existingMeeting.RecurrencePattern = model.RecurrencePattern;
            existingMeeting.RecurrenceEndDate = model.RecurrenceEndDate;
            existingMeeting.RecurrenceNotes = model.RecurrenceNotes;
            existingMeeting.SendCalendarInvite = model.SendCalendarInvite;
            existingMeeting.ReminderBeforeMinutes = model.ReminderBeforeMinutes;
            existingMeeting.SendEmailReminder = model.SendEmailReminder;
            existingMeeting.SendInAppNotification = model.SendInAppNotification;
            existingMeeting.SendSMSReminder = model.SendSMSReminder;
            existingMeeting.AttachedDocuments = model.AttachedDocuments;
            existingMeeting.PresentationLinks = model.PresentationLinks;
            existingMeeting.LastModifiedDate = DateTime.Now;
            existingMeeting.LastModifiedBy = "System"; // In a real app, this would be the logged-in user

            return existingMeeting;
        }

        public async Task DeleteMeetingAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_MeetingSchedulerViewModel? meeting = _meetings.FirstOrDefault(c => c.MeetingId == id);

            if (meeting == null)
            {
                throw new Exception($"Meeting with ID {id} not found");
            }

            _meetings.Remove(meeting);
        }

        public List<string> GetHostTypes() => _hostTypes;
        public List<string> GetMeetingModes() => _meetingModes;
        public List<string> GetMeetingTypes() => _meetingTypes;

        #endregion

        #region Reminder Methods

        public async Task<List<ST_ReminderViewModel>> GetRemindersAsync(int skip = 0, int take = 10, string searchText = "", string? relatedEntityType = null, Guid? relatedEntityId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_ReminderViewModel> query = _reminders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(relatedEntityType) && relatedEntityId.HasValue)
            {
                query = query.Where(c => c.RelatedEntityType == relatedEntityType && c.RelatedEntityId == relatedEntityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ReminderTitle.ToLower().Contains(searchText) ||
                    c.ReminderType.ToLower().Contains(searchText) ||
                    c.UserName.ToLower().Contains(searchText) ||
                    (c.ReminderMessage != null && c.ReminderMessage.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.ReminderDateTime)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetRemindersCountAsync(string searchText = "", string? relatedEntityType = null, Guid? relatedEntityId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_ReminderViewModel> query = _reminders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(relatedEntityType) && relatedEntityId.HasValue)
            {
                query = query.Where(c => c.RelatedEntityType == relatedEntityType && c.RelatedEntityId == relatedEntityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ReminderTitle.ToLower().Contains(searchText) ||
                    c.ReminderType.ToLower().Contains(searchText) ||
                    c.UserName.ToLower().Contains(searchText) ||
                    (c.ReminderMessage != null && c.ReminderMessage.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_ReminderViewModel> GetReminderByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _reminders.FirstOrDefault(c => c.ReminderId == id);
        }

        public async Task<ST_ReminderViewModel> CreateReminderAsync(ST_ReminderViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.ReminderId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.RelatedEntityName = GetEntityName(model.RelatedEntityType, model.RelatedEntityId);
            model.UserName = GetUserName(model.UserId);

            if (model.CustomerContactId.HasValue)
            {
                model.CustomerContactName = GetCustomerName(model.CustomerContactId.Value);
            }

            if (model.SalesTeamId.HasValue)
            {
                model.SalesTeamName = GetSalesTeamName(model.SalesTeamId.Value);
            }

            if (model.AcknowledgedBy.HasValue)
            {
                model.AcknowledgedByName = GetUserName(model.AcknowledgedBy.Value);
            }

            _reminders.Add(model);

            return model;
        }

        public async Task<ST_ReminderViewModel> UpdateReminderAsync(ST_ReminderViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_ReminderViewModel? existingReminder = _reminders.FirstOrDefault(c => c.ReminderId == model.ReminderId);

            if (existingReminder == null)
            {
                throw new Exception($"Reminder with ID {model.ReminderId} not found");
            }

            // Update properties
            existingReminder.ReminderTitle = model.ReminderTitle;
            existingReminder.ReminderType = model.ReminderType;
            existingReminder.RelatedEntityType = model.RelatedEntityType;
            existingReminder.RelatedEntityId = model.RelatedEntityId;
            existingReminder.RelatedEntityName = GetEntityName(model.RelatedEntityType, model.RelatedEntityId);
            existingReminder.ReminderSource = model.ReminderSource;
            existingReminder.ReminderDateTime = model.ReminderDateTime;
            existingReminder.ReminderBefore = model.ReminderBefore;
            existingReminder.RepeatReminder = model.RepeatReminder;
            existingReminder.RepeatIntervalType = model.RepeatIntervalType;
            existingReminder.RepeatCount = model.RepeatCount;
            existingReminder.SendEmail = model.SendEmail;
            existingReminder.SendSMS = model.SendSMS;
            existingReminder.SendInAppNotification = model.SendInAppNotification;
            existingReminder.SendPushNotification = model.SendPushNotification;
            existingReminder.SendWhatsApp = model.SendWhatsApp;
            existingReminder.UserId = model.UserId;
            existingReminder.UserName = GetUserName(model.UserId);
            existingReminder.CustomerContactId = model.CustomerContactId;

            if (model.CustomerContactId.HasValue)
            {
                existingReminder.CustomerContactName = GetCustomerName(model.CustomerContactId.Value);
            }
            else
            {
                existingReminder.CustomerContactName = null;
            }

            existingReminder.IsTeamLevelReminder = model.IsTeamLevelReminder;
            existingReminder.SalesTeamId = model.SalesTeamId;

            if (model.SalesTeamId.HasValue)
            {
                existingReminder.SalesTeamName = GetSalesTeamName(model.SalesTeamId.Value);
            }
            else
            {
                existingReminder.SalesTeamName = null;
            }

            existingReminder.ReminderMessage = model.ReminderMessage;
            existingReminder.ReminderNotes = model.ReminderNotes;
            existingReminder.ReminderLink = model.ReminderLink;
            existingReminder.AttachICSFile = model.AttachICSFile;
            existingReminder.ReminderStatus = model.ReminderStatus;
            existingReminder.AcknowledgedOn = model.AcknowledgedOn;
            existingReminder.AcknowledgedBy = model.AcknowledgedBy;

            if (model.AcknowledgedBy.HasValue)
            {
                existingReminder.AcknowledgedByName = GetUserName(model.AcknowledgedBy.Value);
            }
            else
            {
                existingReminder.AcknowledgedByName = null;
            }

            existingReminder.RetryCount = model.RetryCount;
            existingReminder.Attachments = model.Attachments;
            existingReminder.LastModifiedDate = DateTime.Now;
            existingReminder.LastModifiedBy = "System"; // In a real app, this would be the logged-in user

            return existingReminder;
        }

        public async Task DeleteReminderAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_ReminderViewModel? reminder = _reminders.FirstOrDefault(c => c.ReminderId == id);

            if (reminder == null)
            {
                throw new Exception($"Reminder with ID {id} not found");
            }

            _reminders.Remove(reminder);
        }

        public List<string> GetReminderTypes() => _reminderTypes;
        public List<string> GetReminderSources() => _reminderSources;
        public List<string> GetRepeatIntervalTypes() => _repeatIntervalTypes;
        public List<string> GetReminderStatuses() => _reminderStatuses;

        private string GetOpportunityName(Guid opportunityId)
        {
            return _opportunities.TryGetValue(opportunityId, out string? name) ? name : "Unknown Opportunity";
        }

        #endregion

        #region Outcome Log Methods

        public async Task<List<ST_OutcomeLogViewModel>> GetOutcomeLogsAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_OutcomeLogViewModel> query = _outcomeLogs.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.OutcomeType.ToLower().Contains(searchText) ||
                    c.OutcomeStatus.ToLower().Contains(searchText) ||
                    c.EngagementSummary.ToLower().Contains(searchText) ||
                    (c.DetailedOutcomeNotes != null && c.DetailedOutcomeNotes.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.OutcomeDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetOutcomeLogsCountAsync(string searchText = "", Guid? salesTaskId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_OutcomeLogViewModel> query = _outcomeLogs.AsQueryable();

            if (salesTaskId.HasValue)
            {
                query = query.Where(c => c.SalesTaskId == salesTaskId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.OutcomeType.ToLower().Contains(searchText) ||
                    c.OutcomeStatus.ToLower().Contains(searchText) ||
                    c.EngagementSummary.ToLower().Contains(searchText) ||
                    (c.DetailedOutcomeNotes != null && c.DetailedOutcomeNotes.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_OutcomeLogViewModel> GetOutcomeLogByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _outcomeLogs.FirstOrDefault(c => c.TaskOutcomeLogId == id);
        }

        public async Task<ST_OutcomeLogViewModel> CreateOutcomeLogAsync(ST_OutcomeLogViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.TaskOutcomeLogId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            _outcomeLogs.Add(model);

            return model;
        }

        public async Task<ST_OutcomeLogViewModel> UpdateOutcomeLogAsync(ST_OutcomeLogViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_OutcomeLogViewModel? existingLog = _outcomeLogs.FirstOrDefault(c => c.TaskOutcomeLogId == model.TaskOutcomeLogId);

            if (existingLog == null)
            {
                throw new Exception($"Outcome Log with ID {model.TaskOutcomeLogId} not found");
            }

            // Update properties
            existingLog.OutcomeType = model.OutcomeType;
            existingLog.OutcomeStatus = model.OutcomeStatus;
            existingLog.OutcomeDate = model.OutcomeDate;
            existingLog.NextActionRequired = model.NextActionRequired;
            existingLog.NextFollowUpDueDate = model.NextFollowUpDueDate;
            existingLog.EngagementSummary = model.EngagementSummary;
            existingLog.DetailedOutcomeNotes = model.DetailedOutcomeNotes;
            existingLog.CustomerReaction = model.CustomerReaction;
            existingLog.SalesRepSentiment = model.SalesRepSentiment;
            existingLog.Attachments = model.Attachments;
            existingLog.LastModifiedDate = DateTime.Now;
            existingLog.LastModifiedBy = "System"; // In a real app, this would be the logged-in user

            return existingLog;
        }

        public async Task DeleteOutcomeLogAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_OutcomeLogViewModel? log = _outcomeLogs.FirstOrDefault(c => c.TaskOutcomeLogId == id);

            if (log == null)
            {
                throw new Exception($"Outcome Log with ID {id} not found");
            }

            _outcomeLogs.Remove(log);
        }

        public List<string> GetOutcomeTypes() => _outcomeTypes;
        public List<string> GetOutcomeStatuses() => _outcomeStatuses;
        public List<string> GetCustomerReactions() => _customerReactions;
        public List<string> GetSalesRepSentiments() => _salesRepSentiments;

        #endregion

        #region Activity Audit Trail Methods

        public async Task<List<ST_ActivityAuditTrailViewModel>> GetActivityAuditTrailsAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesActivityId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_ActivityAuditTrailViewModel> query = _activityAuditTrails.AsQueryable();

            if (salesActivityId.HasValue)
            {
                query = query.Where(c => c.SalesActivityId == salesActivityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ChangeType.ToLower().Contains(searchText) ||
                    c.ActionPerformedByUserName.ToLower().Contains(searchText) ||
                    (c.ActionPerformedByRole != null && c.ActionPerformedByRole.ToLower().Contains(searchText)) ||
                    (c.ChangeSummary != null && c.ChangeSummary.ToLower().Contains(searchText)));
            }

            return query.OrderByDescending(c => c.ActionTimestamp)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetActivityAuditTrailsCountAsync(string searchText = "", Guid? salesActivityId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_ActivityAuditTrailViewModel> query = _activityAuditTrails.AsQueryable();

            if (salesActivityId.HasValue)
            {
                query = query.Where(c => c.SalesActivityId == salesActivityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.ChangeType.ToLower().Contains(searchText) ||
                    c.ActionPerformedByUserName.ToLower().Contains(searchText) ||
                    (c.ActionPerformedByRole != null && c.ActionPerformedByRole.ToLower().Contains(searchText)) ||
                    (c.ChangeSummary != null && c.ChangeSummary.ToLower().Contains(searchText)));
            }

            return query.Count();
        }

        public async Task<ST_ActivityAuditTrailViewModel> GetActivityAuditTrailByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _activityAuditTrails.FirstOrDefault(c => c.SalesActivityAuditTrailId == id);
        }

        public async Task<ST_ActivityAuditTrailViewModel> CreateActivityAuditTrailAsync(ST_ActivityAuditTrailViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.SalesActivityAuditTrailId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display names
            model.ActionPerformedByUserName = GetUserName(model.ActionPerformedByUserId);

            if (model.PreviousAssigneeId.HasValue)
            {
                model.PreviousAssigneeName = GetUserName(model.PreviousAssigneeId.Value);
            }

            if (model.NewAssigneeId.HasValue)
            {
                model.NewAssigneeName = GetUserName(model.NewAssigneeId.Value);
            }

            _activityAuditTrails.Add(model);

            return model;
        }

        public List<string> GetChangeTypes() => _changeTypes;
        public List<string> GetActionSources() => _actionSources;

        #endregion

        #region Sales Rep Productivity Metrics Methods

        public async Task<List<ST_SalesRepProductivityMetricsViewModel>> GetSalesRepProductivityMetricsAsync(int skip = 0, int take = 10, string searchText = "", Guid? salesRepUserId = null)
        {
            await Task.Delay(200); // Simulate network delay

            IQueryable<ST_SalesRepProductivityMetricsViewModel> query = _salesRepProductivityMetrics.AsQueryable();

            if (salesRepUserId.HasValue)
            {
                query = query.Where(c => c.SalesRepUserId == salesRepUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.SalesRepUserName.ToLower().Contains(searchText) ||
                    c.EvaluationPeriod.ToLower().Contains(searchText));
            }

            return query.OrderByDescending(c => c.EvaluationDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public async Task<int> GetSalesRepProductivityMetricsCountAsync(string searchText = "", Guid? salesRepUserId = null)
        {
            await Task.Delay(100); // Simulate network delay

            IQueryable<ST_SalesRepProductivityMetricsViewModel> query = _salesRepProductivityMetrics.AsQueryable();

            if (salesRepUserId.HasValue)
            {
                query = query.Where(c => c.SalesRepUserId == salesRepUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(c =>
                    c.SalesRepUserName.ToLower().Contains(searchText) ||
                    c.EvaluationPeriod.ToLower().Contains(searchText));
            }

            return query.Count();
        }

        public async Task<ST_SalesRepProductivityMetricsViewModel> GetSalesRepProductivityMetricByIdAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay
            return _salesRepProductivityMetrics.FirstOrDefault(c => c.SalesRepProductivityId == id);
        }

        public async Task<ST_SalesRepProductivityMetricsViewModel> CreateSalesRepProductivityMetricAsync(ST_SalesRepProductivityMetricsViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            model.SalesRepProductivityId = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = "System"; // In a real app, this would be the logged-in user

            // Add display name
            model.SalesRepUserName = GetUserName(model.SalesRepUserId);

            _salesRepProductivityMetrics.Add(model);

            return model;
        }

        public async Task<ST_SalesRepProductivityMetricsViewModel> UpdateSalesRepProductivityMetricAsync(ST_SalesRepProductivityMetricsViewModel model)
        {
            await Task.Delay(300); // Simulate network delay

            ST_SalesRepProductivityMetricsViewModel? existingMetric = _salesRepProductivityMetrics.FirstOrDefault(c => c.SalesRepProductivityId == model.SalesRepProductivityId);

            if (existingMetric == null)
            {
                throw new Exception($"Sales Rep Productivity Metric with ID {model.SalesRepProductivityId} not found");
            }

            // Update properties
            existingMetric.SalesRepUserId = model.SalesRepUserId;
            existingMetric.SalesRepUserName = GetUserName(model.SalesRepUserId);
            existingMetric.EvaluationDate = model.EvaluationDate;
            existingMetric.EvaluationPeriod = model.EvaluationPeriod;
            existingMetric.TotalTasksAssigned = model.TotalTasksAssigned;
            existingMetric.TasksCompleted = model.TasksCompleted;
            existingMetric.OnTimeCompletionRate = model.OnTimeCompletionRate;
            existingMetric.OverdueTasks = model.OverdueTasks;
            existingMetric.AvgTaskCompletionTimeInMinutes = model.AvgTaskCompletionTimeInMinutes;
            existingMetric.MeetingsHeld = model.MeetingsHeld;
            existingMetric.FollowUpsConducted = model.FollowUpsConducted;
            existingMetric.LeadsConverted = model.LeadsConverted;
            existingMetric.OpportunitiesWon = model.OpportunitiesWon;
            existingMetric.RevenueGenerated = model.RevenueGenerated;
            existingMetric.ConversionRate = model.ConversionRate;
            existingMetric.LeadTouchpointCount = model.LeadTouchpointCount;
            existingMetric.EngagementSuccessRate = model.EngagementSuccessRate;
            existingMetric.LastModifiedDate = DateTime.Now;
            existingMetric.LastModifiedBy = "System"; // In a real app, this would be the logged-in user

            return existingMetric;
        }

        public async Task DeleteSalesRepProductivityMetricAsync(Guid id)
        {
            await Task.Delay(200); // Simulate network delay

            ST_SalesRepProductivityMetricsViewModel? metric = _salesRepProductivityMetrics.FirstOrDefault(c => c.SalesRepProductivityId == id);

            if (metric == null)
            {
                throw new Exception($"Sales Rep Productivity Metric with ID {id} not found");
            }

            _salesRepProductivityMetrics.Remove(metric);
        }

        public List<string> GetEvaluationPeriods() => _evaluationPeriods;

        #endregion

        #region Helper Methods

        // Add method to get opportunities
        public List<KeyValuePair<Guid, string>> GetOpportunities() => _opportunities.Select(o => new KeyValuePair<Guid, string>(o.Key, o.Value)).ToList();

        private string GetUserName(Guid userId)
        {
            return _users.TryGetValue(userId, out string? name) ? name : "Unknown User";
        }

        private string GetRoleName(Guid roleId)
        {
            return _roles.TryGetValue(roleId, out string? name) ? name : "Unknown Role";
        }

        private string GetCampaignName(Guid campaignId)
        {
            return _campaigns.TryGetValue(campaignId, out string? name) ? name : "Unknown Campaign";
        }

        private string GetProductName(Guid productId)
        {
            return _products.TryGetValue(productId, out string? name) ? name : "Unknown Product";
        }

        private string GetCustomerName(Guid customerId)
        {
            return _customers.TryGetValue(customerId, out string? name) ? name : "Unknown Customer";
        }

        private string GetSalesTeamName(Guid salesTeamId)
        {
            return _salesTeams.TryGetValue(salesTeamId, out string? name) ? name : "Unknown Sales Team";
        }

        private string GetEntityName(string entityType, Guid entityId)
        {
            switch (entityType)
            {
                case "Lead":
                case "Customer":
                case "Account":
                case "Contact":
                    return GetCustomerName(entityId);
                case "Campaign":
                    return GetCampaignName(entityId);
                case "SalesTask":
                    ST_MasterViewModel? task = _salesTasks.FirstOrDefault(t => t.TaskId == entityId);
                    return task != null ? task.TaskTitle : "Unknown Task";
                case "Opportunity": // Added case for Opportunity
                    return GetOpportunityName(entityId);
                default:
                    return "Unknown Entity";
            }
        }

        #endregion

        #region Mock Data Initialization

        private void InitializeMockOpportunities()
        {
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC01"), "MYSB Technologies Enterprise Solution");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC02"), "Petronas Digital Transformation Project");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC03"), "MalaysiaAir Cloud Migration");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC04"), "Bank Negara Security Infrastructure");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC05"), "KLCC Property Management System");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC06"), "Axiata 5G Implementation");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC07"), "Digi Communication Platform");
            _opportunities.Add(Guid.Parse("9A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), "Maybank Digital Banking Overhaul");
        }
        private void InitializeMockUsers()
        {
            _users.Add(Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), "Amir bin Abdullah");
            _users.Add(Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), "Farah binti Karim");
            _users.Add(Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), "Rajesh Kumar");
            _users.Add(Guid.Parse("9D65F6A2-F76E-6FE9-AF3E-AC4D53E69AFB"), "Chen Wei Ming");
            _users.Add(Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), "Siti Nur Aisyah");
            _users.Add(Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), "Lim Mei Ling");
            _users.Add(Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), "Abdul Rahman bin Hassan");
            _users.Add(Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), "Vijay Ramasamy");
            _users.Add(Guid.Parse("EEBABEF7-CB9A-BEAE-FB8A-FFAEB8AEBEFF"), "Nurul Huda binti Ismail");
            _users.Add(Guid.Parse("FACBCCF8-DCAB-CCFB-FC9B-FECAC9BFCFFF"), "Ahmad Firdaus bin Malik");
        }

        private void InitializeMockRoles()
        {
            _roles.Add(Guid.Parse("1A23B456-7C89-4D0E-F1F2-F3E4A5B6C7D8"), "Sales Manager");
            _roles.Add(Guid.Parse("2B34C567-8D9A-5E1F-F2F3-E4A5B6C7D8E9"), "Sales Representative");
            _roles.Add(Guid.Parse("3C45D678-9E0F-6F2F-E3A4-B5C6D7E8F9A0"), "Team Lead");
            _roles.Add(Guid.Parse("4D56E789-0F1F-7F3E-A4B5-C6D7E8F9A0B1"), "Account Manager");
            _roles.Add(Guid.Parse("5E67F890-1F2F-8E4A-B5C6-D7E8F9A0B1C2"), "Regional Director");
        }

        private void InitializeMockCampaigns()
        {
            _campaigns.Add(Guid.Parse("A1B2C3D4-E5F6-F7F8-E9A0-B1C2D3E4F5A6"), "Ramadan Special Promotions 2025");
            _campaigns.Add(Guid.Parse("B2C3D4E5-F6F7-F8E9-A0B1-C2D3E4F5A6B7"), "Back to School Campaign 2025");
            _campaigns.Add(Guid.Parse("C3D4E5F6-F7F8-E9A0-B1C2-D3E4F5A6B7C8"), "Digital Transformation Solutions");
            _campaigns.Add(Guid.Parse("D4E5F6F7-F8E9-A0B1-C2D3-E4F5A6B7C8D9"), "SME Growth Package");
            _campaigns.Add(Guid.Parse("E5F6F7F8-E9A0-B1C2-D3E4-F5A6B7C8D9E0"), "Merdeka Day Special Offers");
        }

        private void InitializeMockProducts()
        {
            _products.Add(Guid.Parse("F6F7F8E9-A0B1-C2D3-E4F5-A6B7C8D9E0F1"), "Cloud Enterprise Suite");
            _products.Add(Guid.Parse("F7F8E9A0-B1C2-D3E4-F5A6-B7C8D9E0F1F2"), "Mobile Point-of-Sale System");
            _products.Add(Guid.Parse("F8E9A0B1-C2D3-E4F5-A6B7-C8D9E0F1F2F3"), "Integrated HR Management Solution");
            _products.Add(Guid.Parse("E9A0B1C2-D3E4-F5A6-B7C8-D9E0F1F2F3F4"), "Cybersecurity Essentials Package");
            _products.Add(Guid.Parse("A0B1C2D3-E4F5-A6B7-C8D9-E0F1F2F3F4F5"), "Digital Marketing Analytics Platform");
        }

        private void InitializeMockCustomers()
        {
            _customers.Add(Guid.Parse("B1C2D3E4-F5A6-B7C8-D9E0-F1F2F3F4F5F6"), "Petronas Berhad");
            _customers.Add(Guid.Parse("C2D3E4F5-A6B7-C8D9-E0F1-F2F3F4F5F6A7"), "Maybank Group");
            _customers.Add(Guid.Parse("D3E4F5A6-B7C8-D9E0-F1F2-F3F4F5F6A7B8"), "Sunway Group");
            _customers.Add(Guid.Parse("E4F5A6B7-C8D9-E0F1-F2F3-F4F5F6A7B8C9"), "AirAsia Berhad");
            _customers.Add(Guid.Parse("F5A6B7C8-D9E0-F1F2-F3F4-F5F6A7B8C9D0"), "CIMB Group");
            _customers.Add(Guid.Parse("A6B7C8D9-E0F1-F2F3-F4F5-F6A7B8C9D0E1"), "Maxis Communications");
            _customers.Add(Guid.Parse("B7C8D9E0-F1F2-F3F4-F5F6-A7B8C9D0E1F2"), "Genting Malaysia");
            _customers.Add(Guid.Parse("C8D9E0F1-F2F3-F4F5-F6A7-B8C9D0E1F2F3"), "Top Glove Corporation");
            _customers.Add(Guid.Parse("D9E0F1F2-F3F4-F5F6-A7B8-C9D0E1F2F3F4"), "Tenaga Nasional Berhad");
            _customers.Add(Guid.Parse("E0F1F2F3-F4F5-F6A7-B8C9-D0E1F2F3F4E5"), "IOI Corporation");
        }

        private void InitializeMockSalesTeams()
        {
            _salesTeams.Add(Guid.Parse("F1F2F3F4-F5F6-A7B8-C9D0-E1F2F3F4E5A6"), "Kuala Lumpur Enterprise Team");
            _salesTeams.Add(Guid.Parse("F2F3F4F5-F6A7-B8C9-D0E1-F2F3F4E5A6B7"), "Penang Regional Team");
            _salesTeams.Add(Guid.Parse("F3F4F5F6-A7B8-C9D0-E1F2-F3F4E5A6B7C8"), "Johor Bahru SME Team");
            _salesTeams.Add(Guid.Parse("F4F5F6A7-B8C9-D0E1-F2F3-F4E5A6B7C8D9"), "East Malaysia Enterprise Team");
            _salesTeams.Add(Guid.Parse("F5F6A7B8-C9D0-E1F2-F3F4-E5A6B7C8D9E0"), "Strategic Accounts Team");
        }

        private void InitializeSampleSalesTasks()
        {
            // Add sample sales tasks
            _salesTasks.Add(new ST_MasterViewModel
            {
                TaskId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"),
                TaskCode = "TASK-20250406-001",
                TaskTitle = "Follow-up Call with Petronas Procurement Team",
                TaskDescription = "<p>Schedule a follow-up call with Petronas procurement team to discuss the Cloud Enterprise Suite proposal we sent last week. Focus on the cost savings and integration capabilities.</p><p>Prepare ROI calculations and have technical team on standby for any questions.</p>",
                TaskType = "Call",
                TaskPriority = "High",
                DueDateTime = DateTime.Now.AddDays(2),
                StartDateTime = DateTime.Now.AddDays(1),
                TaskStatus = "Open",
                AssociatedModule = "Customer",
                AssociatedRecordId = Guid.Parse("B1C2D3E4-F5A6-B7C8-D9E0-F1F2F3F4F5F6"), // Petronas
                CreatedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                CreatedByUserName = "Amir bin Abdullah",
                AssignedToUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                AssignedToUserName = "Farah binti Karim",
                IsRecurringTask = false,
                TaskTags = ["FollowUp", "KeyAccount", "CloudSolutions"],
                RelatedProductId = Guid.Parse("F6F7F8E9-A0B1-C2D3-E4F5-A6B7C8D9E0F1"), // Cloud Enterprise Suite
                RelatedProductName = "Cloud Enterprise Suite",
                SourceChannel = "Manual",
                ExpectedOutcome = "Schedule a technical demo for next week",
                HasAttachment = true,
                IsClientFacing = true,
                CustomerVisibilityLevel = "Customer Visible",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System",
                UploadedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "Petronas_Proposal_v2.pdf",
                FileSize = 2450000,
                ContentType = "application/pdf",
                UploadDate = DateTime.Now.AddDays(-2),
                UploadedBy = "Amir bin Abdullah"
            }
                ]
            });

            _salesTasks.Add(new ST_MasterViewModel
            {
                TaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"),
                TaskCode = "TASK-20250407-002",
                TaskTitle = "Prepare Proposal for CIMB Digital Banking Solutions",
                TaskDescription = "<p>Create a comprehensive proposal for CIMB's digital banking transformation initiative. Include case studies from similar implementations, pricing details, and implementation timeline.</p><p>Coordinate with Solution Architects for technical specifications and with Finance for pricing approval.</p>",
                TaskType = "Proposal",
                TaskPriority = "Critical",
                DueDateTime = DateTime.Now.AddDays(5),
                StartDateTime = DateTime.Now,
                TaskStatus = "In Progress",
                AssociatedModule = "Customer",
                AssociatedRecordId = Guid.Parse("F5A6B7C8-D9E0-F1F2-F3F4-F5F6A7B8C9D0"), // CIMB Group
                CreatedByUserId = Guid.Parse("9D65F6A2-F76E-6FE9-AF3E-AC4D53E69AFB"), // Chen
                CreatedByUserName = "Chen Wei Ming",
                AssignedToUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                AssignedToUserName = "Rajesh Kumar",
                AssignedToRoleId = Guid.Parse("2B34C567-8D9A-5E1F-F2F3-E4A5B6C7D8E9"), // Sales Representative
                AssignedToRoleName = "Sales Representative",
                IsRecurringTask = false,
                TaskTags = ["Proposal", "Banking", "DigitalTransformation"],
                RelatedProductId = Guid.Parse("A0B1C2D3-E4F5-A6B7-C8D9-E0F1F2F3F4F5"), // Digital Marketing Analytics Platform
                RelatedProductName = "Digital Marketing Analytics Platform",
                SourceChannel = "Campaign Trigger",
                RelatedCampaignId = Guid.Parse("C3D4E5F6-F7F8-E9A0-B1C2-D3E4F5A6B7C8"), // Digital Transformation Solutions
                RelatedCampaignName = "Digital Transformation Solutions",
                ExpectedOutcome = "Approved proposal and scheduled presentation with CIMB executives",
                HasAttachment = true,
                IsClientFacing = true,
                CustomerVisibilityLevel = "Customer Visible",
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System",
                UploadedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "CIMB_Requirements_Document.docx",
                FileSize = 1850000,
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                UploadDate = DateTime.Now.AddDays(-1),
                UploadedBy = "Chen Wei Ming"
            },
            new DocumentFileInfo
            {
                FileName = "Digital_Banking_Solution_Template.pptx",
                FileSize = 3750000,
                ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                UploadDate = DateTime.Now.AddDays(-1),
                UploadedBy = "Chen Wei Ming"
            }
                ]
            });

            _salesTasks.Add(new ST_MasterViewModel
            {
                TaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"),
                TaskCode = "TASK-20250408-003",
                TaskTitle = "Coordinate Technical Demo for Maxis Communications",
                TaskDescription = "<p>Set up a technical demonstration of our Cybersecurity Essentials Package for Maxis IT security team. Ensure all demo environments are properly configured and test all features before the demo.</p><p>Coordinate with Product Specialists to showcase the latest enhancements.</p>",
                TaskType = "Demo",
                TaskPriority = "High",
                DueDateTime = DateTime.Now.AddDays(3),
                StartDateTime = DateTime.Now.AddDays(2),
                TaskStatus = "Open",
                AssociatedModule = "Customer",
                AssociatedRecordId = Guid.Parse("A6B7C8D9-E0F1-F2F3-F4F5-F6A7B8C9D0E1"), // Maxis Communications
                CreatedByUserId = Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), // Siti
                CreatedByUserName = "Siti Nur Aisyah",
                AssignedToUserId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                AssignedToUserName = "Lim Mei Ling",
                DelegatedToUserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                DelegatedToUserName = "Abdul Rahman bin Hassan",
                IsRecurringTask = false,
                TaskTags = ["TechnicalDemo", "Cybersecurity", "Telco"],
                RelatedProductId = Guid.Parse("E9A0B1C2-D3E4-F5A6-B7C8-D9E0F1F2F3F4"), // Cybersecurity Essentials Package
                RelatedProductName = "Cybersecurity Essentials Package",
                SourceChannel = "Manual",
                ExpectedOutcome = "Successfully demonstrate key cybersecurity features and get commitment for pilot project",
                HasAttachment = false,
                IsClientFacing = true,
                CustomerVisibilityLevel = "Customer Visible",
                CreatedDate = DateTime.Now.AddDays(-3),
                CreatedBy = "System"
            });

            _salesTasks.Add(new ST_MasterViewModel
            {
                TaskId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"),
                TaskCode = "TASK-20250409-004",
                TaskTitle = "Monthly Report for Genting Malaysia Implementation",
                TaskDescription = "<p>Prepare the monthly progress report for the Genting Malaysia Mobile POS implementation. Include milestones achieved, upcoming deliverables, and any risk factors.</p><p>Gather statistics on transaction volume and system performance for inclusion in the report.</p>",
                TaskType = "Internal Review",
                TaskPriority = "Medium",
                DueDateTime = DateTime.Now.AddDays(1),
                StartDateTime = DateTime.Now,
                TaskStatus = "In Progress",
                AssociatedModule = "Customer",
                AssociatedRecordId = Guid.Parse("B7C8D9E0-F1F2-F3F4-F5F6-A7B8C9D0E1F2"), // Genting Malaysia
                CreatedByUserId = Guid.Parse("EEBABEF7-CB9A-BEAE-FB8A-FFAEB8AEBEFF"), // Nurul
                CreatedByUserName = "Nurul Huda binti Ismail",
                AssignedToUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                AssignedToUserName = "Vijay Ramasamy",
                IsRecurringTask = true,
                RecurrencePattern = "Monthly",
                TaskTags = ["MonthlyReport", "Implementation", "Hospitality"],
                RelatedProductId = Guid.Parse("F7F8E9A0-B1C2-D3E4-F5A6-B7C8D9E0F1F2"), // Mobile Point-of-Sale System
                RelatedProductName = "Mobile Point-of-Sale System",
                SourceChannel = "Workflow Automation",
                ExpectedOutcome = "Completed report shared with stakeholders by end of week",
                HasAttachment = true,
                IsClientFacing = false,
                CustomerVisibilityLevel = "Internal Only",
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "System",
                UploadedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "Genting_POS_Implementation_Stats.xlsx",
                FileSize = 1250000,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                UploadDate = DateTime.Now.AddDays(-2),
                UploadedBy = "Vijay Ramasamy"
            }
                ]
            });

            _salesTasks.Add(new ST_MasterViewModel
            {
                TaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"),
                TaskCode = "TASK-20250410-005",
                TaskTitle = "Urgent Issue Resolution for AirAsia HR System",
                TaskDescription = "<p>Address the critical issue reported by AirAsia regarding their HR Management Solution. Users are experiencing slow response times during peak hours.</p><p>Coordinate with technical support team to diagnose and resolve the performance bottleneck.</p>",
                TaskType = "Follow-up",
                TaskPriority = "Critical",
                DueDateTime = DateTime.Now.AddHours(8),
                StartDateTime = DateTime.Now,
                TaskStatus = "In Progress",
                AssociatedModule = "Customer",
                AssociatedRecordId = Guid.Parse("E4F5A6B7-C8D9-E0F1-F2F3-F4F5F6A7B8C9"), // AirAsia Berhad
                CreatedByUserId = Guid.Parse("FACBCCF8-DCAB-CCFB-FC9B-FECAC9BFCFFF"), // Ahmad
                CreatedByUserName = "Ahmad Firdaus bin Malik",
                AssignedToUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                AssignedToUserName = "Farah binti Karim",
                EscalatedToUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                EscalatedToUserName = "Amir bin Abdullah",
                IsRecurringTask = false,
                TaskTags = ["Urgent", "TechnicalIssue", "CustomerSupport"],
                RelatedProductId = Guid.Parse("F8E9A0B1-C2D3-E4F5-A6B7-C8D9E0F1F2F3"), // Integrated HR Management Solution
                RelatedProductName = "Integrated HR Management Solution",
                SourceChannel = "API",
                ExpectedOutcome = "Performance issue resolved and system response time back to normal",
                HasAttachment = true,
                IsClientFacing = true,
                CustomerVisibilityLevel = "Customer Visible",
                CreatedDate = DateTime.Now.AddHours(-4),
                CreatedBy = "System",
                UploadedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "AirAsia_System_Error_Logs.txt",
                FileSize = 750000,
                ContentType = "text/plain",
                UploadDate = DateTime.Now.AddHours(-3),
                UploadedBy = "Ahmad Firdaus bin Malik"
            },
            new DocumentFileInfo
            {
                FileName = "Performance_Monitoring_Screenshot.png",
                FileSize = 980000,
                ContentType = "image/png",
                UploadDate = DateTime.Now.AddHours(-2),
                UploadedBy = "Farah binti Karim"
            }
                ]
            });
        }

        private void InitializeSampleAssignmentLogs()
        {
            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("6F7F8F9F-0A1A-2B3B-4C5C-6D7D8D9D0D1D"),
                SalesTaskId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                ActionType = "Assigned",
                ActionTimestamp = DateTime.Now.AddDays(-2),
                ToUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                ToUserName = "Farah binti Karim",
                IsSystemGenerated = false,
                PerformedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                PerformedByUserName = "Amir bin Abdullah",
                AssignmentNote = "Initial assignment for Petronas follow-up call",
                TaskPriorityAtAssignment = "High",
                DueDateAtAssignment = DateTime.Now.AddDays(2),
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddDays(-2),
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("7F8F9F0A-1A2A-3B4B-5C6C-7D8D9D0D1D2D"),
                SalesTaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                ActionType = "Assigned",
                ActionTimestamp = DateTime.Now.AddDays(-1),
                ToUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                ToUserName = "Rajesh Kumar",
                IsSystemGenerated = false,
                PerformedByUserId = Guid.Parse("9D65F6A2-F76E-6FE9-AF3E-AC4D53E69AFB"), // Chen
                PerformedByUserName = "Chen Wei Ming",
                AssignmentNote = "Please handle the CIMB proposal with highest priority",
                TaskPriorityAtAssignment = "Critical",
                DueDateAtAssignment = DateTime.Now.AddDays(5),
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddDays(-1),
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("8F9F0A1A-2A3A-4B5B-6C7C-8D9D0D1D2D3D"),
                SalesTaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                ActionType = "Assigned",
                ActionTimestamp = DateTime.Now.AddDays(-3),
                ToUserId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                ToUserName = "Lim Mei Ling",
                IsSystemGenerated = false,
                PerformedByUserId = Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), // Siti
                PerformedByUserName = "Siti Nur Aisyah",
                AssignmentNote = "Coordinate the technical demo setup",
                TaskPriorityAtAssignment = "High",
                DueDateAtAssignment = DateTime.Now.AddDays(3),
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddDays(-3),
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now.AddDays(-3),
                CreatedBy = "System"
            });

            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("9F0A1A2A-3A4A-5B6B-7C8C-9D0D1D2D3D4D"),
                SalesTaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                ActionType = "Delegated",
                ActionTimestamp = DateTime.Now.AddDays(-2),
                FromUserId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                FromUserName = "Lim Mei Ling",
                ToUserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                ToUserName = "Abdul Rahman bin Hassan",
                IsSystemGenerated = false,
                PerformedByUserId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                PerformedByUserName = "Lim Mei Ling",
                AssignmentNote = "Delegating as I'll be on leave. Please handle the technical preparation.",
                TaskPriorityAtAssignment = "High",
                DueDateAtAssignment = DateTime.Now.AddDays(3),
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddDays(-2),
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("0A1A2A3A-4A5A-6B7B-8C9C-0D1D2D3D4D5D"),
                SalesTaskId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                ActionType = "Assigned",
                ActionTimestamp = DateTime.Now.AddDays(-5),
                ToUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                ToUserName = "Vijay Ramasamy",
                IsSystemGenerated = true,
                PerformedByUserId = Guid.Parse("EEBABEF7-CB9A-BEAE-FB8A-FFAEB8AEBEFF"), // Nurul
                PerformedByUserName = "Nurul Huda binti Ismail",
                AssignmentNote = "Auto-assigned monthly reporting task",
                TaskPriorityAtAssignment = "Medium",
                DueDateAtAssignment = DateTime.Now.AddDays(1),
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddDays(-5),
                AssignmentChannel = "WorkflowRule",
                TriggerSource = "Monthly Reporting Automation",
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "System"
            });

            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("1A2A3A4A-5A6A-7B8B-9C0C-1D2D3D4D5D6D"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                ActionType = "Assigned",
                ActionTimestamp = DateTime.Now.AddHours(-4),
                ToUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                ToUserName = "Farah binti Karim",
                IsSystemGenerated = false,
                PerformedByUserId = Guid.Parse("FACBCCF8-DCAB-CCFB-FC9B-FECAC9BFCFFF"), // Ahmad
                PerformedByUserName = "Ahmad Firdaus bin Malik",
                AssignmentNote = "Urgent issue with AirAsia HR system",
                TaskPriorityAtAssignment = "Critical",
                DueDateAtAssignment = DateTime.Now.AddHours(8),
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddHours(-4),
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now.AddHours(-4),
                CreatedBy = "System"
            });

            _assignmentLogs.Add(new ST_AssignmentLogViewModel
            {
                LogId = Guid.Parse("2A3A4A5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                ActionType = "Escalated",
                ActionTimestamp = DateTime.Now.AddHours(-2),
                FromUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                FromUserName = "Farah binti Karim",
                ToUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                ToUserName = "Amir bin Abdullah",
                IsSystemGenerated = false,
                PerformedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                PerformedByUserName = "Farah binti Karim",
                AssignmentNote = "Escalating to manager as issue requires system adjustment beyond my access level",
                TaskPriorityAtAssignment = "Critical",
                DueDateAtAssignment = DateTime.Now.AddHours(8),
                EscalationLevel = 1,
                IsNotified = true,
                NotificationTimestamp = DateTime.Now.AddHours(-2),
                AssignmentChannel = "Manual",
                CreatedDate = DateTime.Now.AddHours(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleStatusTimelines()
        {
            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("3A4A5A6A-7A8A-9B0B-1C2C-3D4D5D6D7D8D"),
                SalesTaskId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                PreviousStatus = "New",
                NewStatus = "Open",
                StatusChangedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                StatusChangedByUserName = "Amir bin Abdullah",
                StatusChangedOn = DateTime.Now.AddDays(-2),
                StatusChangeNote = "Task created and opened",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                TotalDurationFromStart = TimeSpan.Zero,
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("4A5A6A7A-8A9A-0B1B-2C3C-4D5D6D7D8D9D"),
                SalesTaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                PreviousStatus = "New",
                NewStatus = "Open",
                StatusChangedByUserId = Guid.Parse("9D65F6A2-F76E-6FE9-AF3E-AC4D53E69AFB"), // Chen
                StatusChangedByUserName = "Chen Wei Ming",
                StatusChangedOn = DateTime.Now.AddDays(-1).AddHours(-3),
                StatusChangeNote = "Task created and assigned",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                TotalDurationFromStart = TimeSpan.Zero,
                CreatedDate = DateTime.Now.AddDays(-1).AddHours(-3),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("5A6A7A8A-9A0A-1B2B-3C4C-5D6D7D8D9D0D"),
                SalesTaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                PreviousStatus = "Open",
                NewStatus = "In Progress",
                StatusChangedByUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                StatusChangedByUserName = "Rajesh Kumar",
                StatusChangedOn = DateTime.Now.AddDays(-1),
                StatusChangeNote = "Starting work on the proposal",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                DurationFromPreviousStatus = TimeSpan.FromHours(3),
                TotalDurationFromStart = TimeSpan.FromHours(3),
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("6A7A8A9A-0A1A-2B3B-4C5C-6D7D8D9D0D1D"),
                SalesTaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                PreviousStatus = "New",
                NewStatus = "Open",
                StatusChangedByUserId = Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), // Siti
                StatusChangedByUserName = "Siti Nur Aisyah",
                StatusChangedOn = DateTime.Now.AddDays(-3),
                StatusChangeNote = "Task created for Maxis demo",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                TotalDurationFromStart = TimeSpan.Zero,
                CreatedDate = DateTime.Now.AddDays(-3),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("7A8A9A0A-1A2A-3B4B-5C6C-7D8D9D0D1D2D"),
                SalesTaskId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                PreviousStatus = "New",
                NewStatus = "Open",
                StatusChangedByUserId = Guid.Parse("EEBABEF7-CB9A-BEAE-FB8A-FFAEB8AEBEFF"), // Nurul
                StatusChangedByUserName = "Nurul Huda binti Ismail",
                StatusChangedOn = DateTime.Now.AddDays(-5),
                StatusChangeNote = "Monthly report task created",
                StatusChangeChannel = "WorkflowRule",
                IsSystemGenerated = true,
                TriggerSource = "MonthlyReportAutomation",
                IsFinalStatus = false,
                TotalDurationFromStart = TimeSpan.Zero,
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("8A9A0A1A-2A3A-4B5B-6C7C-8D9D0D1D2D3D"),
                SalesTaskId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                PreviousStatus = "Open",
                NewStatus = "In Progress",
                StatusChangedByUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                StatusChangedByUserName = "Vijay Ramasamy",
                StatusChangedOn = DateTime.Now.AddDays(-2),
                StatusChangeNote = "Started gathering data for the report",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                DurationFromPreviousStatus = TimeSpan.FromDays(3),
                TotalDurationFromStart = TimeSpan.FromDays(3),
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("9A0A1A2A-3A4A-5B6B-7C8C-9D0D1D2D3D4D"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                PreviousStatus = "New",
                NewStatus = "Open",
                StatusChangedByUserId = Guid.Parse("FACBCCF8-DCAB-CCFB-FC9B-FECAC9BFCFFF"), // Ahmad
                StatusChangedByUserName = "Ahmad Firdaus bin Malik",
                StatusChangedOn = DateTime.Now.AddHours(-4),
                StatusChangeNote = "Urgent issue created",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                TotalDurationFromStart = TimeSpan.Zero,
                CreatedDate = DateTime.Now.AddHours(-4),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("0B1B2B3B-4B5B-6C7C-8D9D-0E1E2E3E4E5E"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                PreviousStatus = "Open",
                NewStatus = "In Progress",
                StatusChangedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                StatusChangedByUserName = "Farah binti Karim",
                StatusChangedOn = DateTime.Now.AddHours(-3),
                StatusChangeNote = "Started investigating the performance issue",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                DurationFromPreviousStatus = TimeSpan.FromHours(1),
                TotalDurationFromStart = TimeSpan.FromHours(1),
                CreatedDate = DateTime.Now.AddHours(-3),
                CreatedBy = "System"
            });

            _statusTimelines.Add(new ST_StatusTimelineViewModel
            {
                StatusTimelineId = Guid.Parse("1B2B3B4B-5B6B-7C8C-9D0D-1E2E3E4E5E6E"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                PreviousStatus = "In Progress",
                NewStatus = "Escalated",
                StatusChangedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                StatusChangedByUserName = "Farah binti Karim",
                StatusChangedOn = DateTime.Now.AddHours(-2),
                StatusChangeNote = "Escalated to manager for system-level access",
                StatusChangeChannel = "Manual",
                IsSystemGenerated = false,
                RelatedActionId = Guid.Parse("2A3A4A5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // Related assignment log
                TriggerSource = "UserAction",
                IsFinalStatus = false,
                DurationFromPreviousStatus = TimeSpan.FromHours(1),
                TotalDurationFromStart = TimeSpan.FromHours(2),
                CreatedDate = DateTime.Now.AddHours(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleLogs()
        {
            _logs.Add(new ST_LogViewModel
            {
                ActivityLogId = Guid.Parse("2B3B4B5B-6B7B-8C9C-0D1D-2E3E4E5E6E7E"),
                SalesTaskId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                PerformedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                PerformedByUserName = "Farah binti Karim",
                PerformedOn = DateTime.Now.AddDays(-1),
                ActivityType = "Call",
                ActivityChannel = "ManualEntry",
                ActivityDescription = "Called Mr. Zulkifli from Petronas procurement to discuss our proposal. He confirmed receipt and is reviewing it with his team.",
                NextActionDate = DateTime.Now.AddDays(2),
                ActivityResult = "Call Answered - Positive Response",
                IsCustomerVisible = true,
                IsImportantFlagged = true,
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _logs.Add(new ST_LogViewModel
            {
                ActivityLogId = Guid.Parse("3B4B5B6B-7B8B-9C0C-1D2D-3E4E5E6E7E8E"),
                SalesTaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                PerformedByUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                PerformedByUserName = "Rajesh Kumar",
                PerformedOn = DateTime.Now.AddHours(-6),
                ActivityType = "Email",
                ActivityChannel = "ManualEntry",
                ActivityDescription = "Sent an email to Ms. Lee at CIMB requesting additional information about their technical environment for the proposal.",
                NextActionDate = DateTime.Now.AddDays(1),
                ActivityResult = "Email Sent - Awaiting Response",
                IsCustomerVisible = true,
                IsImportantFlagged = false,
                ReferenceEmailId = Guid.Parse("A1B2C3D4-E5F6-A7B8-C9D0-E1F2A3B4C5D6"), // Mock email ID
                CreatedDate = DateTime.Now.AddHours(-6),
                CreatedBy = "System"
            });

            _logs.Add(new ST_LogViewModel
            {
                ActivityLogId = Guid.Parse("4B5B6B7B-8B9B-0C1C-2D3D-4E5E6E7E8E9E"),
                SalesTaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                PerformedByUserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                PerformedByUserName = "Abdul Rahman bin Hassan",
                PerformedOn = DateTime.Now.AddDays(-1),
                ActivityType = "Note",
                ActivityChannel = "ManualEntry",
                ActivityDescription = "Coordinated with technical team to prepare demo environment. Completed setup of all security modules for the Maxis demonstration.",
                NextActionDate = null,
                ActivityResult = "Setup Complete",
                IsCustomerVisible = false,
                IsImportantFlagged = false,
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _logs.Add(new ST_LogViewModel
            {
                ActivityLogId = Guid.Parse("5B6B7B8B-9B0B-1C2C-3D4D-5E6E7E8E9E0E"),
                SalesTaskId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                PerformedByUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                PerformedByUserName = "Vijay Ramasamy",
                PerformedOn = DateTime.Now.AddHours(-12),
                ActivityType = "FileUpload",
                ActivityChannel = "WebApp",
                ActivityDescription = "Uploaded the latest transaction statistics for the Genting POS implementation to include in the monthly report.",
                NextActionDate = null,
                ActivityResult = "File Uploaded",
                IsCustomerVisible = false,
                IsImportantFlagged = false,
                AttachmentUrl = "https://intranet.company.com/files/genting_stats.xlsx",
                AssociatedDocumentId = Guid.Parse("B2C3D4E5-F6A7-B8C9-D0E1-F2A3B4C5D6E7"), // Mock document ID
                CreatedDate = DateTime.Now.AddHours(-12),
                CreatedBy = "System",
                UploadedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "Genting_Transaction_Stats_April.xlsx",
                FileSize = 1450000,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                UploadDate = DateTime.Now.AddHours(-12),
                UploadedBy = "Vijay Ramasamy"
            }
                ]
            });

            _logs.Add(new ST_LogViewModel
            {
                ActivityLogId = Guid.Parse("6B7B8B9B-0B1B-2C3C-4D5D-6E7E8E9E0E1E"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                PerformedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                PerformedByUserName = "Farah binti Karim",
                PerformedOn = DateTime.Now.AddHours(-3),
                ActivityType = "Call",
                ActivityChannel = "ManualEntry",
                ActivityDescription = "Called AirAsia IT support to gather more details about the performance issue. They reported that system slows down significantly during peak hours (9-11 AM).",
                NextActionDate = null,
                ActivityResult = "Call Answered - Gathered Information",
                IsCustomerVisible = true,
                IsImportantFlagged = true,
                CreatedDate = DateTime.Now.AddHours(-3),
                CreatedBy = "System"
            });

            _logs.Add(new ST_LogViewModel
            {
                ActivityLogId = Guid.Parse("7B8B9B0B-1B2B-3C4C-5D6D-7E8E9E0E1E2E"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                PerformedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                PerformedByUserName = "Farah binti Karim",
                PerformedOn = DateTime.Now.AddHours(-2),
                ActivityType = "EscalationNote",
                ActivityChannel = "ManualEntry",
                ActivityDescription = "Escalated to Amir as the issue requires database optimization that needs management approval.",
                NextActionDate = null,
                ActivityResult = "Escalated",
                IsCustomerVisible = false,
                IsImportantFlagged = true,
                CreatedDate = DateTime.Now.AddHours(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleFollowUps()
        {
            _followUps.Add(new ST_FollowUpTrackerViewModel
            {
                FollowUpId = Guid.Parse("8B9B0B1B-2B3B-4C5C-6D7D-8E9E0E1E2E3E"),
                SalesTaskId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("B1C2D3E4-F5A6-B7C8-D9E0-F1F2F3F4F5F6"), // Petronas
                RelatedEntityName = "Petronas Berhad",
                ScheduledDateTime = DateTime.Now.AddDays(2).AddHours(10), // 10 AM
                DueDateTime = DateTime.Now.AddDays(2).AddHours(17), // 5 PM
                FollowUpReason = "Discuss Cloud Suite Proposal",
                ReminderBeforeInMinutes = 30,
                IsRecurring = false,
                AssignedToUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                AssignedToUserName = "Farah binti Karim",
                AssignedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                AssignedByUserName = "Amir bin Abdullah",
                IsSelfAssigned = false,
                FollowUpStatus = "Pending",
                OutcomeNotes = "",
                CustomerResponseSummary = "",
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = false,
                NotificationSent = false,
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _followUps.Add(new ST_FollowUpTrackerViewModel
            {
                FollowUpId = Guid.Parse("9B0B1B2B-3B4B-5C6C-7D8D-9E0E1E2E3E4E"),
                SalesTaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("F5A6B7C8-D9E0-F1F2-F3F4-F5F6A7B8C9D0"), // CIMB Group
                RelatedEntityName = "CIMB Group",
                ScheduledDateTime = DateTime.Now.AddDays(1).AddHours(14), // 2 PM
                DueDateTime = DateTime.Now.AddDays(1).AddHours(17), // 5 PM
                FollowUpReason = "Check on Additional Information Request",
                ReminderBeforeInMinutes = 60,
                IsRecurring = false,
                AssignedToUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                AssignedToUserName = "Rajesh Kumar",
                AssignedByUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                AssignedByUserName = "Rajesh Kumar",
                IsSelfAssigned = true,
                FollowUpStatus = "Pending",
                OutcomeNotes = "",
                CustomerResponseSummary = "",
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = false,
                NotificationSent = false,
                CreatedDate = DateTime.Now.AddHours(-6),
                CreatedBy = "System"
            });

            _followUps.Add(new ST_FollowUpTrackerViewModel
            {
                FollowUpId = Guid.Parse("0C1C2C3C-4C5C-6D7D-8E9E-0F1F2F3F4F5F"),
                SalesTaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("A6B7C8D9-E0F1-F2F3-F4F5-F6A7B8C9D0E1"), // Maxis Communications
                RelatedEntityName = "Maxis Communications",
                ScheduledDateTime = DateTime.Now.AddDays(3).AddHours(13).AddMinutes(30), // 1:30 PM
                DueDateTime = DateTime.Now.AddDays(3).AddHours(17), // 5 PM
                FollowUpReason = "Technical Demo Confirmation",
                ReminderBeforeInMinutes = 120,
                IsRecurring = false,
                AssignedToUserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                AssignedToUserName = "Abdul Rahman bin Hassan",
                AssignedByUserId = Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), // Siti
                AssignedByUserName = "Siti Nur Aisyah",
                IsSelfAssigned = false,
                FollowUpStatus = "Pending",
                OutcomeNotes = "",
                CustomerResponseSummary = "",
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = false,
                NotificationSent = false,
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _followUps.Add(new ST_FollowUpTrackerViewModel
            {
                FollowUpId = Guid.Parse("1C2C3C4C-5C6C-7D8D-9E0E-1F2F3F4F5F6F"),
                SalesTaskId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("B7C8D9E0-F1F2-F3F4-F5F6-A7B8C9D0E1F2"), // Genting Malaysia
                RelatedEntityName = "Genting Malaysia",
                ScheduledDateTime = DateTime.Now.AddDays(7).AddHours(14), // 2 PM
                FollowUpReason = "Monthly Report Review",
                ReminderBeforeInMinutes = 60,
                IsRecurring = true,
                RecurrencePattern = "Monthly",
                RecurrenceEndDate = DateTime.Now.AddYears(1),
                AssignedToUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                AssignedToUserName = "Vijay Ramasamy",
                AssignedByUserId = Guid.Parse("EEBABEF7-CB9A-BEAE-FB8A-FFAEB8AEBEFF"), // Nurul
                AssignedByUserName = "Nurul Huda binti Ismail",
                IsSelfAssigned = false,
                FollowUpStatus = "Pending",
                OutcomeNotes = "",
                CustomerResponseSummary = "",
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = false,
                NotificationSent = false,
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "System"
            });

            _followUps.Add(new ST_FollowUpTrackerViewModel
            {
                FollowUpId = Guid.Parse("2C3C4C5C-6C7C-8D9D-0E1E-2F3F4F5F6F7F"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("E4F5A6B7-C8D9-E0F1-F2F3-F4F5F6A7B8C9"), // AirAsia Berhad
                RelatedEntityName = "AirAsia Berhad",
                ScheduledDateTime = DateTime.Now.AddHours(8), // 8 hours from now
                DueDateTime = DateTime.Now.AddHours(10), // 10 hours from now
                FollowUpReason = "Performance Fix Confirmation",
                ReminderBeforeInMinutes = 15,
                IsRecurring = false,
                AssignedToUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                AssignedToUserName = "Amir bin Abdullah",
                AssignedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                AssignedByUserName = "Farah binti Karim",
                IsSelfAssigned = false,
                FollowUpStatus = "Pending",
                OutcomeNotes = "",
                CustomerResponseSummary = "",
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = true,
                NotificationSent = false,
                CreatedDate = DateTime.Now.AddHours(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleMeetings()
        {
            _meetings.Add(new ST_MeetingSchedulerViewModel
            {
                MeetingId = Guid.Parse("3C4C5C6C-7C8C-9D0D-1E2E-3F4F5F6F7F8F"),
                MeetingTitle = "Petronas Cloud Enterprise Suite Demo",
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("B1C2D3E4-F5A6-B7C8-D9E0-F1F2F3F4F5F6"), // Petronas
                RelatedEntityName = "Petronas Berhad",
                MeetingDate = DateTime.Now.AddDays(5).AddHours(10), // 10 AM
                DurationInMinutes = 90,
                Timezone = "Asia/Kuala_Lumpur",
                IsFullDayEvent = false,
                OrganizerUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                OrganizerUserName = "Farah binti Karim",
                ParticipantUserIds =
                [
                    Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
            Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA") // Rajesh
                ],
                ParticipantUserNames =
                [
                    "Amir bin Abdullah",
            "Rajesh Kumar"
                ],
                ExternalParticipants =
                [
                    "zulkifli@petronas.com",
            "ahmad.razak@petronas.com",
            "sarah.tan@petronas.com"
                ],
                HostType = "Joint",
                MeetingWithCustomerId = Guid.Parse("B1C2D3E4-F5A6-B7C8-D9E0-F1F2F3F4F5F6"), // Petronas
                MeetingWithCustomerName = "Petronas Berhad",
                MeetingMode = "Online",
                MeetingLink = "https://teams.microsoft.com/l/meetup-join/12345",
                MeetingAgenda = "1. Introduction (10 mins)\n2. Cloud Enterprise Suite Demo (45 mins)\n3. Integration Capabilities (20 mins)\n4. ROI Discussion (10 mins)\n5. Q&A (5 mins)",
                MeetingObjective = "Demonstrate the Cloud Enterprise Suite and secure commitment for a pilot project",
                MeetingType = "Demo",
                PreparationChecklist =
                [
                    "Prepare demo environment",
            "Test all features",
            "Prepare ROI slides",
            "Review Petronas IT infrastructure"
                ],
                IsRecurring = false,
                SendCalendarInvite = true,
                ReminderBeforeMinutes = 30,
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = false,
                AttachedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "Cloud_Enterprise_Demo_Deck.pptx",
                FileSize = 3250000,
                ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                UploadDate = DateTime.Now.AddDays(-1),
                UploadedBy = "Farah binti Karim"
            }
                ],
                PresentationLinks =
                [
                    "https://company.sharepoint.com/presentations/cloud_enterprise_demo"
                ],
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _meetings.Add(new ST_MeetingSchedulerViewModel
            {
                MeetingId = Guid.Parse("4C5C6C7C-8C9C-0D1D-2E3E-4F5F6F7F8F9F"),
                MeetingTitle = "CIMB Digital Banking Proposal Presentation",
                RelatedEntityType = "Customer",
                RelatedEntityId = Guid.Parse("F5A6B7C8-D9E0-F1F2-F3F4-F5F6A7B8C9D0"), // CIMB Group
                RelatedEntityName = "CIMB Group",
                MeetingDate = DateTime.Now.AddDays(7).AddHours(14), // 2 PM
                DurationInMinutes = 120,
                Timezone = "Asia/Kuala_Lumpur",
                IsFullDayEvent = false,
                OrganizerUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                OrganizerUserName = "Rajesh Kumar",
                ParticipantUserIds =
                [
                    Guid.Parse("9D65F6A2-F76E-6FE9-AF3E-AC4D53E69AFB"), // Chen
            Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC") // Siti
                ],
                ParticipantUserNames =
                [
                    "Chen Wei Ming",
            "Siti Nur Aisyah"
                ],
                ExternalParticipants =
                [
                    "lee.mei@cimb.com",
            "tan.sri.dato@cimb.com",
            "james.wong@cimb.com",
            "azizah.hassan@cimb.com"
                ],
                HostType = "Internal",
                MeetingWithCustomerId = Guid.Parse("F5A6B7C8-D9E0-F1F2-F3F4-F5F6A7B8C9D0"), // CIMB Group
                MeetingWithCustomerName = "CIMB Group",
                MeetingMode = "Hybrid",
                MeetingLocation = "CIMB Headquarters, Menara CIMB, Jalan Stesen Sentral 2, Kuala Lumpur",
                MeetingLink = "https://zoom.us/j/123456789",
                MapCoordinates = "3.1349,101.6841",
                MeetingAgenda = "1. Executive Summary (15 mins)\n2. Digital Banking Transformation Solution (40 mins)\n3. Implementation Timeline (20 mins)\n4. Cost & ROI Analysis (30 mins)\n5. Discussion & Next Steps (15 mins)",
                MeetingObjective = "Present the complete digital banking transformation proposal and secure approval for the project",
                MeetingType = "Pitch",
                PreparationChecklist =
                [
                    "Finalize presentation",
            "Print handouts",
            "Prepare demo account",
            "Confirm technical requirements at venue"
                ],
                IsRecurring = false,
                SendCalendarInvite = true,
                ReminderBeforeMinutes = 60,
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = true,
                AttachedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "CIMB_Digital_Banking_Proposal.pdf",
                FileSize = 4850000,
                ContentType = "application/pdf",
                UploadDate = DateTime.Now.AddDays(-1),
                UploadedBy = "Rajesh Kumar"
            },
            new DocumentFileInfo
            {
                FileName = "Digital_Banking_Presentation.pptx",
                FileSize = 7250000,
                ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                UploadDate = DateTime.Now,
                UploadedBy = "Rajesh Kumar"
            }
                ],
                PresentationLinks =
                [
                    "https://company.sharepoint.com/presentations/cimb_digital_banking"
                ],
                CreatedDate = DateTime.Now.AddDays(-3),
                CreatedBy = "System"
            });

            _meetings.Add(new ST_MeetingSchedulerViewModel
            {
                MeetingId = Guid.Parse("5C6C7C8C-9C0C-1D2D-3E4E-5F6F7F8F9F0F"),
                MeetingTitle = "Maxis Cybersecurity Solution Demo",
                RelatedEntityType = "SalesTask",
                RelatedEntityId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                RelatedEntityName = "Coordinate Technical Demo for Maxis Communications",
                MeetingDate = DateTime.Now.AddDays(3).AddHours(15), // 3 PM
                DurationInMinutes = 120,
                Timezone = "Asia/Kuala_Lumpur",
                IsFullDayEvent = false,
                OrganizerUserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                OrganizerUserName = "Abdul Rahman bin Hassan",
                ParticipantUserIds =
                [
                    Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), // Siti
            Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF") // Vijay
                ],
                ParticipantUserNames =
                [
                    "Siti Nur Aisyah",
            "Vijay Ramasamy"
                ],
                ExternalParticipants =
                [
                    "ahmad.zaki@maxis.com.my",
            "tan.cheng.li@maxis.com.my",
            "rahmat.ibrahim@maxis.com.my",
            "jasmine.wong@maxis.com.my"
                ],
                HostType = "Internal",
                MeetingWithCustomerId = Guid.Parse("A6B7C8D9-E0F1-F2F3-F4F5-F6A7B8C9D0E1"), // Maxis Communications
                MeetingWithCustomerName = "Maxis Communications",
                MeetingMode = "Online",
                MeetingLink = "https://webex.com/meet/12345",
                MeetingAgenda = "1. Introduction (10 mins)\n2. Cybersecurity Challenges Overview (15 mins)\n3. Solution Demonstration (60 mins)\n4. Implementation Approach (20 mins)\n5. Q&A (15 mins)",
                MeetingObjective = "Demonstrate our Cybersecurity Essentials Package and address Maxis' security concerns",
                MeetingType = "Demo",
                PreparationChecklist =
                [
                    "Set up demo environment",
            "Prepare custom Maxis security scenarios",
            "Test all firewall and threat detection features",
            "Review Maxis' previous security incidents"
                ],
                IsRecurring = false,
                SendCalendarInvite = true,
                ReminderBeforeMinutes = 45,
                SendEmailReminder = true,
                SendInAppNotification = true,
                SendSMSReminder = false,
                AttachedDocuments =
                [
                    new DocumentFileInfo
            {
                FileName = "Cybersecurity_Demo_Script.docx",
                FileSize = 1750000,
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                UploadDate = DateTime.Now.AddDays(-1),
                UploadedBy = "Abdul Rahman bin Hassan"
            }
                ],
                PresentationLinks =
                [
                    "https://company.sharepoint.com/presentations/cybersecurity_demo"
                ],
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleReminders()
        {
            _reminders.Add(new ST_ReminderViewModel
            {
                ReminderId = Guid.Parse("6C7C8C9C-0C1C-2D3D-4E5E-6F7F8F9F0F1F"),
                ReminderTitle = "Prepare for Petronas Call",
                ReminderType = "Task",
                RelatedEntityType = "SalesTask",
                RelatedEntityId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                RelatedEntityName = "Follow-up Call with Petronas Procurement Team",
                ReminderSource = "Manual",
                ReminderDateTime = DateTime.Now.AddDays(1).AddHours(15), // One day before the actual call
                ReminderBefore = 60, // 1 hour before
                RepeatReminder = false,
                SendEmail = true,
                SendInAppNotification = true,
                SendSMS = false,
                SendPushNotification = false,
                SendWhatsApp = false,
                UserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                UserName = "Farah binti Karim",
                ReminderMessage = "Prepare for the Petronas follow-up call tomorrow. Review the proposal and ROI calculations.",
                ReminderNotes = "Focus on cost savings and integration capabilities",
                ReminderLink = "/SalesTask/Master/View?id=1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D",
                AttachICSFile = false,
                ReminderStatus = "Scheduled",
                RetryCount = 0,
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _reminders.Add(new ST_ReminderViewModel
            {
                ReminderId = Guid.Parse("7C8C9C0C-1C2C-3D4D-5E6E-7F8F9F0F1F2F"),
                ReminderTitle = "CIMB Follow-up Call",
                ReminderType = "FollowUp",
                RelatedEntityType = "SalesTask",
                RelatedEntityId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                RelatedEntityName = "Prepare Proposal for CIMB Digital Banking Solutions",
                ReminderSource = "Manual",
                ReminderDateTime = DateTime.Now.AddDays(1).AddHours(13), // 1 PM, one hour before scheduled follow-up
                RepeatReminder = true,
                RepeatIntervalType = "Hourly",
                RepeatCount = 3,
                SendEmail = true,
                SendInAppNotification = true,
                SendSMS = true,
                SendPushNotification = true,
                SendWhatsApp = false,
                UserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                UserName = "Rajesh Kumar",
                ReminderMessage = "Follow up with Ms. Lee at CIMB regarding the technical environment information request",
                ReminderNotes = "Critical for completing the proposal",
                ReminderLink = "/SalesTask/Master/View?id=2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D",
                AttachICSFile = true,
                ReminderStatus = "Scheduled",
                RetryCount = 0,
                CreatedDate = DateTime.Now.AddHours(-6),
                CreatedBy = "System"
            });

            _reminders.Add(new ST_ReminderViewModel
            {
                ReminderId = Guid.Parse("8C9C0C1C-2C3C-4D5D-6E7E-8F9F0F1F2F3F"),
                ReminderTitle = "Maxis Demo Preparations",
                ReminderType = "Meeting",
                RelatedEntityType = "SalesTask",
                RelatedEntityId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                RelatedEntityName = "Coordinate Technical Demo for Maxis Communications",
                ReminderSource = "Manual",
                ReminderDateTime = DateTime.Now.AddDays(2).AddHours(9), // 9 AM, one day before demo
                ReminderBefore = 120, // 2 hours before
                RepeatReminder = false,
                SendEmail = true,
                SendInAppNotification = true,
                SendSMS = false,
                SendPushNotification = false,
                SendWhatsApp = false,
                UserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                UserName = "Abdul Rahman bin Hassan",
                IsTeamLevelReminder = true,
                SalesTeamId = Guid.Parse("F4F5F6A7-B8C9-D0E1-F2F3-F4E5A6B7C8D9"), // East Malaysia Enterprise Team
                SalesTeamName = "East Malaysia Enterprise Team",
                ReminderMessage = "Final preparations for Maxis Cybersecurity Demo. Ensure all demo environments are ready and tested.",
                ReminderNotes = "Coordinate with product specialists for final checks",
                ReminderLink = "/SalesTask/Master/View?id=3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D",
                AttachICSFile = false,
                ReminderStatus = "Scheduled",
                RetryCount = 0,
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _reminders.Add(new ST_ReminderViewModel
            {
                ReminderId = Guid.Parse("9C0C1C2C-3C4C-5D6D-7E8E-9F0F1F2F3F4F"),
                ReminderTitle = "Genting Monthly Report Submission",
                ReminderType = "Task",
                RelatedEntityType = "SalesTask",
                RelatedEntityId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                RelatedEntityName = "Monthly Report for Genting Malaysia Implementation",
                ReminderSource = "Recurring",
                ReminderDateTime = DateTime.Now.AddDays(1).AddHours(9), // 9 AM on due date
                RepeatReminder = true,
                RepeatIntervalType = "Daily",
                RepeatCount = 1,
                SendEmail = true,
                SendInAppNotification = true,
                SendSMS = false,
                SendPushNotification = true,
                SendWhatsApp = false,
                UserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                UserName = "Vijay Ramasamy",
                ReminderMessage = "Submit the monthly progress report for Genting Malaysia Mobile POS implementation today",
                ReminderNotes = "Include transaction volume statistics",
                ReminderLink = "/SalesTask/Master/View?id=4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D",
                AttachICSFile = false,
                ReminderStatus = "Scheduled",
                RetryCount = 0,
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _reminders.Add(new ST_ReminderViewModel
            {
                ReminderId = Guid.Parse("0D1D2D3D-4D5D-6D7D-8D9D-0E1E2E3E4E5E"),
                ReminderTitle = "URGENT: AirAsia HR System Issue",
                ReminderType = "Task",
                RelatedEntityType = "SalesTask",
                RelatedEntityId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                RelatedEntityName = "Urgent Issue Resolution for AirAsia HR System",
                ReminderSource = "Manual",
                ReminderDateTime = DateTime.Now.AddHours(7), // 1 hour before deadline
                RepeatReminder = true,
                RepeatIntervalType = "Hourly",
                RepeatCount = 3,
                SendEmail = true,
                SendInAppNotification = true,
                SendSMS = true,
                SendPushNotification = true,
                SendWhatsApp = true,
                UserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                UserName = "Amir bin Abdullah",
                CustomerContactId = Guid.Parse("E4F5A6B7-C8D9-E0F1-F2F3-F4F5F6A7B8C9"), // AirAsia
                CustomerContactName = "AirAsia Berhad",
                ReminderMessage = "CRITICAL: Resolve AirAsia HR System performance issue by deadline",
                ReminderNotes = "Issue escalated, requires immediate attention",
                ReminderLink = "/SalesTask/Master/View?id=5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D",
                AttachICSFile = false,
                ReminderStatus = "Sent",
                AcknowledgedOn = DateTime.Now.AddHours(-1),
                AcknowledgedBy = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                AcknowledgedByName = "Amir bin Abdullah",
                RetryCount = 0,
                CreatedDate = DateTime.Now.AddHours(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleOutcomeLogs()
        {
            _outcomeLogs.Add(new ST_OutcomeLogViewModel
            {
                TaskOutcomeLogId = Guid.Parse("1D2D3D4D-5D6D-7D8D-9D0D-1E2E3E4E5E6E"),
                SalesTaskId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                OutcomeType = "CallAnswered",
                OutcomeStatus = "Success",
                OutcomeDate = DateTime.Now.AddDays(-1),
                NextActionRequired = true,
                NextFollowUpDueDate = DateTime.Now.AddDays(2),
                EngagementSummary = "Spoke with Mr. Zulkifli who confirmed receipt of our proposal",
                DetailedOutcomeNotes = "Mr. Zulkifli mentioned that the procurement team is reviewing our proposal and they're particularly interested in the cost savings section. He requested more information about integration with their existing SAP systems.",
                CustomerReaction = "Positive",
                SalesRepSentiment = "Confident",
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _outcomeLogs.Add(new ST_OutcomeLogViewModel
            {
                TaskOutcomeLogId = Guid.Parse("2D3D4D5D-6D7D-8D9D-0E1E-2E3E4E5E6E7E"),
                SalesTaskId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                OutcomeType = "Objection",
                OutcomeStatus = "Partial",
                OutcomeDate = DateTime.Now.AddHours(-12),
                NextActionRequired = true,
                NextFollowUpDueDate = DateTime.Now.AddDays(1),
                EngagementSummary = "Discussed digital banking proposal outline with Ms. Lee",
                DetailedOutcomeNotes = "Ms. Lee expressed concerns about the timeline and integration complexity. She needs more technical details before proceeding further. Need to prepare a technical integration document to address her concerns.",
                CustomerReaction = "Objection",
                SalesRepSentiment = "Need Help",
                CreatedDate = DateTime.Now.AddHours(-12),
                CreatedBy = "System",
                Attachments =
                [
                    new DocumentFileInfo
            {
                FileName = "CIMB_Call_Notes.docx",
                FileSize = 550000,
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                UploadDate = DateTime.Now.AddHours(-12),
                UploadedBy = "Rajesh Kumar"
            }
                ]
            });

            _outcomeLogs.Add(new ST_OutcomeLogViewModel
            {
                TaskOutcomeLogId = Guid.Parse("3D4D5D6D-7D8D-9D0D-1E2E-3E4E5E6E7E8E"),
                SalesTaskId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                OutcomeType = "MeetingHeld",
                OutcomeStatus = "Success",
                OutcomeDate = DateTime.Now.AddDays(-1),
                NextActionRequired = true,
                NextFollowUpDueDate = DateTime.Now.AddDays(3),
                EngagementSummary = "Preliminary meeting to discuss cybersecurity demo requirements",
                DetailedOutcomeNotes = "Met with Maxis IT security team to understand their specific requirements for the demo. They emphasized the need to show how our solution addresses threat detection for their specific telecom infrastructure. Demo environment setup is in progress.",
                CustomerReaction = "Positive",
                SalesRepSentiment = "Confident",
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _outcomeLogs.Add(new ST_OutcomeLogViewModel
            {
                TaskOutcomeLogId = Guid.Parse("4D5D6D7D-8D9D-0E1E-2E3E-4E5E6E7E8E9E"),
                SalesTaskId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                OutcomeType = "Rescheduled",
                OutcomeStatus = "PendingFollowUp",
                OutcomeDate = DateTime.Now.AddHours(-3),
                NextActionRequired = true,
                NextFollowUpDueDate = DateTime.Now.AddHours(4),
                EngagementSummary = "Initial diagnostic of HR system performance issue",
                DetailedOutcomeNotes = "Identified potential database query performance issue during peak hours. Need to further investigate indexing strategy and connection pooling. Escalated to manager for system-level access to make necessary adjustments.",
                CustomerReaction = "Neutral",
                SalesRepSentiment = "Uncertain",
                CreatedDate = DateTime.Now.AddHours(-3),
                CreatedBy = "System",
                Attachments =
                [
                    new DocumentFileInfo
            {
                FileName = "AirAsia_HR_System_Diagnostic.pdf",
                FileSize = 1250000,
                ContentType = "application/pdf",
                UploadDate = DateTime.Now.AddHours(-3),
                UploadedBy = "Farah binti Karim"
            }
                ]
            });
        }

        private void InitializeSampleActivityAuditTrails()
        {
            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("5D6D7D8D-9D0D-1E2E-3E4E-5E6E7E8E9E0E"),
                SalesActivityId = Guid.Parse("1F2F3F4F-5A6A-7B8B-9C0C-1D2D3D4D5D6D"), // Petronas task
                ChangeType = "Created",
                ActionPerformedByUserId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                ActionPerformedByUserName = "Amir bin Abdullah",
                ActionPerformedByRole = "Sales Manager",
                ActionTimestamp = DateTime.Now.AddDays(-2),
                ActionSource = "WebApp",
                NewStatus = "Open",
                NewAssigneeId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                NewAssigneeName = "Farah binti Karim",
                NewDueDate = DateTime.Now.AddDays(2),
                ChangeSummary = "Created follow-up task for Petronas",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("6D7D8D9D-0D1D-2E3E-4E5E-6E7E8E9E0E1E"),
                SalesActivityId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                ChangeType = "Created",
                ActionPerformedByUserId = Guid.Parse("9D65F6A2-F76E-6FE9-AF3E-AC4D53E69AFB"), // Chen
                ActionPerformedByUserName = "Chen Wei Ming",
                ActionPerformedByRole = "Account Manager",
                ActionTimestamp = DateTime.Now.AddDays(-1).AddHours(-3),
                ActionSource = "WebApp",
                NewStatus = "Open",
                NewAssigneeId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                NewAssigneeName = "Rajesh Kumar",
                NewDueDate = DateTime.Now.AddDays(5),
                ChangeSummary = "Created proposal task for CIMB digital banking",
                TriggeringEntity = "CustomerID: F5A6B7C8-D9E0-F1F2-F3F4-F5F6A7B8C9D0", // CIMB Group
                CreatedDate = DateTime.Now.AddDays(-1).AddHours(-3),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("7D8D9D0D-1D2D-3E4E-5E6E-7E8E9E0E1E2E"),
                SalesActivityId = Guid.Parse("2F3F4F5A-6A7A-8B9B-0C1C-2D3D4D5D6D7D"), // CIMB task
                ChangeType = "StatusChanged",
                ActionPerformedByUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                ActionPerformedByUserName = "Rajesh Kumar",
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now.AddDays(-1),
                ActionSource = "WebApp",
                PreviousStatus = "Open",
                NewStatus = "In Progress",
                ChangeSummary = "Started working on CIMB proposal",
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("8D9D0D1D-2D3D-4E5E-6E7E-8E9E0E1E2E3E"),
                SalesActivityId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                ChangeType = "Created",
                ActionPerformedByUserId = Guid.Parse("AE76F7B3-E87F-7FFA-BF4F-BD5E64F7ABFC"), // Siti
                ActionPerformedByUserName = "Siti Nur Aisyah",
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now.AddDays(-3),
                ActionSource = "WebApp",
                NewStatus = "Open",
                NewAssigneeId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                NewAssigneeName = "Lim Mei Ling",
                NewDueDate = DateTime.Now.AddDays(3),
                ChangeSummary = "Created task for Maxis cybersecurity demo",
                CreatedDate = DateTime.Now.AddDays(-3),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("9D0D1D2D-3D4D-5E6E-7E8E-9E0E1E2E3E4E"),
                SalesActivityId = Guid.Parse("3F4F5F6F-7A8A-9B0B-1C2C-3D4D5D6D7D8D"), // Maxis task
                ChangeType = "Reassigned",
                ActionPerformedByUserId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                ActionPerformedByUserName = "Lim Mei Ling",
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now.AddDays(-2),
                ActionSource = "WebApp",
                PreviousAssigneeId = Guid.Parse("BF87F8C4-E98F-8FFB-CF5F-CE6F75F8BCFD"), // Lim
                PreviousAssigneeName = "Lim Mei Ling",
                NewAssigneeId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                NewAssigneeName = "Abdul Rahman bin Hassan",
                ChangeSummary = "Delegated task to Abdul as Lim will be on leave",
                ActionRemarks = "Please handle the technical preparation while I'm away",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("0E1E2E3E-4E5E-6E7E-8E9E-0F1F2F3F4F5F"),
                SalesActivityId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                ChangeType = "Created",
                ActionPerformedByUserId = Guid.Parse("EEBABEF7-CB9A-BEAE-FB8A-FFAEB8AEBEFF"), // Nurul
                ActionPerformedByUserName = "Nurul Huda binti Ismail",
                ActionPerformedByRole = "Team Lead",
                ActionTimestamp = DateTime.Now.AddDays(-5),
                ActionSource = "WorkflowEngine",
                NewStatus = "Open",
                NewAssigneeId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                NewAssigneeName = "Vijay Ramasamy",
                NewDueDate = DateTime.Now.AddDays(1),
                ChangeSummary = "Auto-created monthly report task",
                RequiresFollowUp = false,
                TriggeringEntity = "WorkflowRuleID: Monthly_Report_Generation",
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("1E2E3E4E-5E6E-7E8E-9E0E-1F2F3F4F5F6F"),
                SalesActivityId = Guid.Parse("4F5F6F7F-8A9A-0B1B-2C3C-4D5D6D7D8D9D"), // Genting task
                ChangeType = "StatusChanged",
                ActionPerformedByUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                ActionPerformedByUserName = "Vijay Ramasamy",
                ActionPerformedByRole = "Sales Representative",
                ActionTimestamp = DateTime.Now.AddDays(-2),
                ActionSource = "WebApp",
                PreviousStatus = "Open",
                NewStatus = "In Progress",
                ChangeSummary = "Started working on Genting monthly report",
                CreatedDate = DateTime.Now.AddDays(-2),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("2E3E4E5E-6E7E-8E9E-0F1F-2F3F4F5F6F7F"),
                SalesActivityId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                ChangeType = "Created",
                ActionPerformedByUserId = Guid.Parse("FACBCCF8-DCAB-CCFB-FC9B-FECAC9BFCFFF"), // Ahmad
                ActionPerformedByUserName = "Ahmad Firdaus bin Malik",
                ActionPerformedByRole = "Support Specialist",
                ActionTimestamp = DateTime.Now.AddHours(-4),
                ActionSource = "API",
                NewStatus = "Open",
                NewAssigneeId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                NewAssigneeName = "Farah binti Karim",
                NewDueDate = DateTime.Now.AddHours(8),
                ChangeSummary = "Created urgent task for AirAsia HR system issue",
                RequiresFollowUp = true,
                TriggeringEntity = "SupportTicketID: AirAsia-HR-20250410-001",
                CreatedDate = DateTime.Now.AddHours(-4),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("3E4E5E6E-7E8E-9E0E-1F2F-3F4F5F6F7F8F"),
                SalesActivityId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                ChangeType = "StatusChanged",
                ActionPerformedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                ActionPerformedByUserName = "Farah binti Karim",
                ActionPerformedByRole = "Technical Consultant",
                ActionTimestamp = DateTime.Now.AddHours(-3),
                ActionSource = "WebApp",
                PreviousStatus = "Open",
                NewStatus = "In Progress",
                ChangeSummary = "Started investigating AirAsia HR system issue",
                CreatedDate = DateTime.Now.AddHours(-3),
                CreatedBy = "System"
            });

            _activityAuditTrails.Add(new ST_ActivityAuditTrailViewModel
            {
                SalesActivityAuditTrailId = Guid.Parse("4E5E6E7E-8E9E-0F1F-2F3F-4F5F6F7F8F9F"),
                SalesActivityId = Guid.Parse("5F6F7F8F-9A0A-1B2B-3C4C-5D6D7D8D9D0D"), // AirAsia task
                ChangeType = "Escalated",
                ActionPerformedByUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                ActionPerformedByUserName = "Farah binti Karim",
                ActionPerformedByRole = "Technical Consultant",
                ActionTimestamp = DateTime.Now.AddHours(-2),
                ActionSource = "WebApp",
                PreviousStatus = "In Progress",
                NewStatus = "Escalated",
                PreviousAssigneeId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                PreviousAssigneeName = "Farah binti Karim",
                NewAssigneeId = Guid.Parse("6A32E379-D3A8-4BB6-9C1A-7637F59ADC08"), // Amir
                NewAssigneeName = "Amir bin Abdullah",
                ChangeSummary = "Escalated AirAsia HR system issue to manager",
                ActionRemarks = "Need system-level access to resolve the performance bottleneck",
                RequiresFollowUp = true,
                CreatedDate = DateTime.Now.AddHours(-2),
                CreatedBy = "System"
            });
        }

        private void InitializeSampleSalesRepProductivityMetrics()
        {
            _salesRepProductivityMetrics.Add(new ST_SalesRepProductivityMetricsViewModel
            {
                SalesRepProductivityId = Guid.Parse("5E6E7E8E-9E0E-1F2F-3F4F-5F6F7F8F9F0F"),
                SalesRepUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                SalesRepUserName = "Farah binti Karim",
                EvaluationDate = DateTime.Now.AddDays(-1),
                EvaluationPeriod = "Daily",
                TotalTasksAssigned = 12,
                TasksCompleted = 10,
                OnTimeCompletionRate = 95.5M,
                OverdueTasks = 1,
                AvgTaskCompletionTimeInMinutes = 186,
                MeetingsHeld = 3,
                FollowUpsConducted = 8,
                LeadsConverted = 2,
                OpportunitiesWon = 1,
                RevenueGenerated = 250000.00M, // RM 250,000
                ConversionRate = 50.0M,
                LeadTouchpointCount = 4,
                EngagementSuccessRate = 85.7M,
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _salesRepProductivityMetrics.Add(new ST_SalesRepProductivityMetricsViewModel
            {
                SalesRepProductivityId = Guid.Parse("6E7E8E9E-0E1E-2F3F-4F5F-6F7F8F9F0F1F"),
                SalesRepUserId = Guid.Parse("7B43F480-E54C-4DC7-8D1C-8A2B31D478F9"), // Farah
                SalesRepUserName = "Farah binti Karim",
                EvaluationDate = DateTime.Now.AddDays(-30),
                EvaluationPeriod = "Monthly",
                TotalTasksAssigned = 78,
                TasksCompleted = 72,
                OnTimeCompletionRate = 92.3M,
                OverdueTasks = 6,
                AvgTaskCompletionTimeInMinutes = 205,
                MeetingsHeld = 24,
                FollowUpsConducted = 65,
                LeadsConverted = 12,
                OpportunitiesWon = 6,
                RevenueGenerated = 1250000.00M, // RM 1,250,000
                ConversionRate = 50.0M,
                LeadTouchpointCount = 5,
                EngagementSuccessRate = 82.5M,
                CreatedDate = DateTime.Now.AddDays(-30),
                CreatedBy = "System"
            });

            _salesRepProductivityMetrics.Add(new ST_SalesRepProductivityMetricsViewModel
            {
                SalesRepProductivityId = Guid.Parse("7E8E9E0E-1E2E-3F4F-5F6F-7F8F9F0F1F2F"),
                SalesRepUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                SalesRepUserName = "Rajesh Kumar",
                EvaluationDate = DateTime.Now.AddDays(-1),
                EvaluationPeriod = "Daily",
                TotalTasksAssigned = 8,
                TasksCompleted = 7,
                OnTimeCompletionRate = 87.5M,
                OverdueTasks = 1,
                AvgTaskCompletionTimeInMinutes = 220,
                MeetingsHeld = 2,
                FollowUpsConducted = 5,
                LeadsConverted = 1,
                OpportunitiesWon = 0,
                RevenueGenerated = 0.00M,
                ConversionRate = 0.0M,
                LeadTouchpointCount = 3,
                EngagementSuccessRate = 75.0M,
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _salesRepProductivityMetrics.Add(new ST_SalesRepProductivityMetricsViewModel
            {
                SalesRepProductivityId = Guid.Parse("8E9E0E1E-2E3E-4F5F-6F7F-8F9F0F1F2F3F"),
                SalesRepUserId = Guid.Parse("8C54E591-F65D-5ED8-9E2D-9B3C42D589EA"), // Rajesh
                SalesRepUserName = "Rajesh Kumar",
                EvaluationDate = DateTime.Now.AddDays(-30),
                EvaluationPeriod = "Monthly",
                TotalTasksAssigned = 62,
                TasksCompleted = 58,
                OnTimeCompletionRate = 93.5M,
                OverdueTasks = 4,
                AvgTaskCompletionTimeInMinutes = 195,
                MeetingsHeld = 18,
                FollowUpsConducted = 55,
                LeadsConverted = 10,
                OpportunitiesWon = 7,
                RevenueGenerated = 1850000.00M, // RM 1,850,000
                ConversionRate = 70.0M,
                LeadTouchpointCount = 6,
                EngagementSuccessRate = 85.2M,
                CreatedDate = DateTime.Now.AddDays(-30),
                CreatedBy = "System"
            });

            _salesRepProductivityMetrics.Add(new ST_SalesRepProductivityMetricsViewModel
            {
                SalesRepProductivityId = Guid.Parse("9E0E1E2E-3E4E-5F6F-7F8F-9F0F1F2F3F4F"),
                SalesRepUserId = Guid.Parse("DFA9AAE6-BA9E-AAED-EA7E-EF8FA7EADAEF"), // Vijay
                SalesRepUserName = "Vijay Ramasamy",
                EvaluationDate = DateTime.Now.AddDays(-1),
                EvaluationPeriod = "Daily",
                TotalTasksAssigned = 6,
                TasksCompleted = 5,
                OnTimeCompletionRate = 83.3M,
                OverdueTasks = 1,
                AvgTaskCompletionTimeInMinutes = 210,
                MeetingsHeld = 1,
                FollowUpsConducted = 4,
                LeadsConverted = 0,
                OpportunitiesWon = 0,
                RevenueGenerated = 0.00M,
                ConversionRate = 0.0M,
                LeadTouchpointCount = 2,
                EngagementSuccessRate = 80.0M,
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });

            _salesRepProductivityMetrics.Add(new ST_SalesRepProductivityMetricsViewModel
            {
                SalesRepProductivityId = Guid.Parse("0F1F2F3F-4F5F-6F7F-8F9F-0A1A2A3A4A5A"),
                SalesRepUserId = Guid.Parse("CF98E9D5-A98F-9EFC-DE6F-DF7F86F9DCEE"), // Abdul
                SalesRepUserName = "Abdul Rahman bin Hassan",
                EvaluationDate = DateTime.Now.AddDays(-1),
                EvaluationPeriod = "Daily",
                TotalTasksAssigned = 10,
                TasksCompleted = 9,
                OnTimeCompletionRate = 90.0M,
                OverdueTasks = 1,
                AvgTaskCompletionTimeInMinutes = 175,
                MeetingsHeld = 2,
                FollowUpsConducted = 7,
                LeadsConverted = 1,
                OpportunitiesWon = 1,
                RevenueGenerated = 125000.00M, // RM 125,000
                ConversionRate = 100.0M,
                LeadTouchpointCount = 4,
                EngagementSuccessRate = 88.2M,
                CreatedDate = DateTime.Now.AddDays(-1),
                CreatedBy = "System"
            });
        }
        #endregion
    }
}