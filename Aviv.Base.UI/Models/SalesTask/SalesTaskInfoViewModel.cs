using System.ComponentModel.DataAnnotations;

namespace Aviv.Base.UI.Models.SalesTask
{
    // Main Sales Task model
    public class SalesTaskInfoViewModel
    {
        // Master Section
        public ST_MasterViewModel Master { get; set; } = new ST_MasterViewModel();

        // Related records
        public List<ST_AssignmentLogViewModel> AssignmentLogs { get; set; } = [];
        public List<ST_StatusTimelineViewModel> StatusTimelines { get; set; } = [];
        public List<ST_LogViewModel> Logs { get; set; } = [];
        public List<ST_FollowUpTrackerViewModel> FollowUps { get; set; } = [];
        public List<ST_MeetingSchedulerViewModel> Meetings { get; set; } = [];
        public List<ST_ReminderViewModel> Reminders { get; set; } = [];
        public List<ST_OutcomeLogViewModel> OutcomeLogs { get; set; } = [];
        public List<ST_ActivityAuditTrailViewModel> ActivityAuditTrails { get; set; } = [];
        public List<ST_SalesRepProductivityMetricsViewModel> ProductivityMetrics { get; set; } = [];
    }

    // 1. Master - Detail Section
    public class ST_MasterViewModel
    {
        // a. Detail
        public Guid TaskId { get; set; }
        public string? TaskCode { get; set; }

        [Required(ErrorMessage = "Task title is required")]
        public string? TaskTitle { get; set; }

        public string? TaskDescription { get; set; }

        [Required(ErrorMessage = "Task type is required")]
        public string? TaskType { get; set; }

        [Required(ErrorMessage = "Task priority is required")]
        public string? TaskPriority { get; set; }

        [Required(ErrorMessage = "Due date/time is required")]
        public DateTime? DueDateTime { get; set; }

        public DateTime? StartDateTime { get; set; }

        [Required(ErrorMessage = "Task status is required")]
        public string? TaskStatus { get; set; }

        [Required(ErrorMessage = "Associated module is required")]
        public string? AssociatedModule { get; set; }

        public Guid? AssociatedRecordId { get; set; }

        // b. Assignment & Ownership
        public Guid CreatedByUserId { get; set; }
        public string? CreatedByUserName { get; set; } // For display purposes

        [Required(ErrorMessage = "Assigned to user is required")]
        public Guid AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; } // For display purposes

        public Guid? AssignedToRoleId { get; set; }
        public string? AssignedToRoleName { get; set; } // For display purposes

        public Guid? DelegatedToUserId { get; set; }
        public string? DelegatedToUserName { get; set; } // For display purposes

        public Guid? EscalatedToUserId { get; set; }
        public string? EscalatedToUserName { get; set; } // For display purposes

        public bool IsRecurringTask { get; set; }
        public string? RecurrencePattern { get; set; }
        public List<string> TaskTags { get; set; } = [];

        // c. Metadata & Context
        public Guid? RelatedCampaignId { get; set; }
        public string? RelatedCampaignName { get; set; } // For display purposes

        public Guid? RelatedProductId { get; set; }
        public string? RelatedProductName { get; set; } // For display purposes

        public string? SourceChannel { get; set; }
        public string? ExpectedOutcome { get; set; }
        public bool HasAttachment { get; set; }
        public bool IsClientFacing { get; set; }
        public string? CustomerVisibilityLevel { get; set; }
        public string? CustomFieldsJson { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }

        // Document attachments
        public List<DocumentFileInfo> UploadedDocuments { get; set; } = [];
    }

    // 2. Assignment Log
    public class ST_AssignmentLogViewModel
    {
        public Guid LogId { get; set; }
        public Guid SalesTaskId { get; set; }

        [Required(ErrorMessage = "Action type is required")]
        public string? ActionType { get; set; }

        [Required(ErrorMessage = "Action timestamp is required")]
        public DateTime? ActionTimestamp { get; set; }

        public Guid? FromUserId { get; set; }
        public string? FromUserName { get; set; } // For display purposes

        [Required(ErrorMessage = "To user is required")]
        public Guid ToUserId { get; set; }
        public string? ToUserName { get; set; } // For display purposes

        public bool IsSystemGenerated { get; set; }

        [Required(ErrorMessage = "Performed by user is required")]
        public Guid PerformedByUserId { get; set; }
        public string? PerformedByUserName { get; set; } // For display purposes

        public string? AssignmentNote { get; set; }
        public string? TaskPriorityAtAssignment { get; set; }
        public DateTime? DueDateAtAssignment { get; set; }
        public int? EscalationLevel { get; set; }
        public bool IsNotified { get; set; }
        public DateTime? NotificationTimestamp { get; set; }
        public string? AssignmentChannel { get; set; }
        public string? TriggerSource { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    // 3. Status Timeline
    public class ST_StatusTimelineViewModel
    {
        public Guid StatusTimelineId { get; set; }
        public Guid SalesTaskId { get; set; }

        [Required(ErrorMessage = "Previous status is required")]
        public string? PreviousStatus { get; set; }

        [Required(ErrorMessage = "New status is required")]
        public string? NewStatus { get; set; }

        [Required(ErrorMessage = "Status changed by user is required")]
        public Guid StatusChangedByUserId { get; set; }
        public string? StatusChangedByUserName { get; set; } // For display purposes

        [Required(ErrorMessage = "Status changed on date is required")]
        public DateTime? StatusChangedOn { get; set; }

        public string? StatusChangeNote { get; set; }
        public string? StatusChangeChannel { get; set; }
        public bool IsSystemGenerated { get; set; }
        public Guid? RelatedActionId { get; set; }
        public string? TriggerSource { get; set; }
        public bool IsFinalStatus { get; set; }
        public TimeSpan? DurationFromPreviousStatus { get; set; }
        public TimeSpan? TotalDurationFromStart { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    // 4. Log
    public class ST_LogViewModel
    {
        public Guid ActivityLogId { get; set; }
        public Guid SalesTaskId { get; set; }

        [Required(ErrorMessage = "Performed by user is required")]
        public Guid PerformedByUserId { get; set; }
        public string? PerformedByUserName { get; set; } // For display purposes

        [Required(ErrorMessage = "Performed on date is required")]
        public DateTime? PerformedOn { get; set; }

        [Required(ErrorMessage = "Activity type is required")]
        public string? ActivityType { get; set; }

        public string? ActivityChannel { get; set; }

        [Required(ErrorMessage = "Activity description is required")]
        public string? ActivityDescription { get; set; }

        public DateTime? NextActionDate { get; set; }
        public string? ActivityResult { get; set; }
        public bool IsCustomerVisible { get; set; }
        public bool IsImportantFlagged { get; set; }

        // Attachments/References
        public string? AttachmentUrl { get; set; }
        public string? ExternalMeetingLink { get; set; }
        public Guid? ReferenceEmailId { get; set; }
        public Guid? AssociatedDocumentId { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        // Document attachments
        public List<DocumentFileInfo> UploadedDocuments { get; set; } = [];
    }

    // 5. Follow-Up Tracker
    public class ST_FollowUpTrackerViewModel
    {
        // a. Core Identification
        public Guid FollowUpId { get; set; }
        public Guid SalesTaskId { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "Related entity type is required")]
        public string? RelatedEntityType { get; set; }

        [Required(ErrorMessage = "Related entity ID is required")]
        public Guid RelatedEntityId { get; set; }
        public string? RelatedEntityName { get; set; } // For display purposes

        // b. Follow-Up Timing & Triggers
        [Required(ErrorMessage = "Scheduled date/time is required")]
        public DateTime? ScheduledDateTime { get; set; }

        public DateTime? DueDateTime { get; set; }

        [Required(ErrorMessage = "Follow-up reason is required")]
        public string? FollowUpReason { get; set; }

        public int? ReminderBeforeInMinutes { get; set; }
        public bool IsRecurring { get; set; }

        // c. Recurrence Settings (if IsRecurring)
        public string? RecurrencePattern { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? CustomRecurrenceDetails { get; set; }

        // d. Assigned & Ownership Info
        [Required(ErrorMessage = "Assigned to user is required")]
        public Guid AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; } // For display purposes

        [Required(ErrorMessage = "Assigned by user is required")]
        public Guid AssignedByUserId { get; set; }
        public string? AssignedByUserName { get; set; } // For display purposes

        public bool IsSelfAssigned { get; set; }

        // e. Status & Notes
        [Required(ErrorMessage = "Follow-up status is required")]
        public string? FollowUpStatus { get; set; }

        public string? OutcomeNotes { get; set; }
        public string? CustomerResponseSummary { get; set; }

        // f. Notification & Alerts
        public bool SendEmailReminder { get; set; }
        public bool SendInAppNotification { get; set; }
        public bool SendSMSReminder { get; set; }
        public bool NotificationSent { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    // 6. Meeting Scheduler
    public class ST_MeetingSchedulerViewModel
    {
        // a. Details
        public Guid MeetingId { get; set; }

        [Required(ErrorMessage = "Meeting title is required")]
        public string? MeetingTitle { get; set; }

        [Required(ErrorMessage = "Related entity type is required")]
        public string? RelatedEntityType { get; set; }

        public Guid? RelatedEntityId { get; set; }
        public string? RelatedEntityName { get; set; } // For display purposes

        // b. Date, Time & Duration
        [Required(ErrorMessage = "Meeting date is required")]
        public DateTime? MeetingDate { get; set; }

        [Required(ErrorMessage = "Duration in minutes is required")]
        public int DurationInMinutes { get; set; }

        public string? Timezone { get; set; }
        public bool IsFullDayEvent { get; set; }

        // c. Participants & Roles
        [Required(ErrorMessage = "Organizer user is required")]
        public Guid OrganizerUserId { get; set; }
        public string? OrganizerUserName { get; set; } // For display purposes

        public List<Guid> ParticipantUserIds { get; set; } = [];
        public List<string> ParticipantUserNames { get; set; } = []; // For display purposes

        public List<string> ExternalParticipants { get; set; } = [];
        public string? HostType { get; set; }
        public Guid? MeetingWithCustomerId { get; set; }
        public string? MeetingWithCustomerName { get; set; } // For display purposes

        // d. Location, Link & Mode
        [Required(ErrorMessage = "Meeting mode is required")]
        public string? MeetingMode { get; set; }

        public string? MeetingLocation { get; set; }
        public string? MeetingLink { get; set; }
        public string? MapCoordinates { get; set; }

        // e. Agenda & Objectives
        [Required(ErrorMessage = "Meeting agenda is required")]
        public string? MeetingAgenda { get; set; }

        public string? MeetingObjective { get; set; }

        [Required(ErrorMessage = "Meeting type is required")]
        public string? MeetingType { get; set; }

        public List<string> PreparationChecklist { get; set; } = [];

        // f. Recurrence & Series
        public bool IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? RecurrenceNotes { get; set; }

        // g. Notifications & Reminders
        public bool SendCalendarInvite { get; set; }
        public int? ReminderBeforeMinutes { get; set; }
        public bool SendEmailReminder { get; set; }
        public bool SendInAppNotification { get; set; }
        public bool SendSMSReminder { get; set; }

        // h. Attachments & Docs
        public List<DocumentFileInfo> AttachedDocuments { get; set; } = [];
        public List<string> PresentationLinks { get; set; } = [];

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    // 7. Reminder
    public class ST_ReminderViewModel
    {
        // a. Details
        public Guid ReminderId { get; set; }

        [Required(ErrorMessage = "Reminder title is required")]
        public string? ReminderTitle { get; set; }

        [Required(ErrorMessage = "Reminder type is required")]
        public string? ReminderType { get; set; }

        [Required(ErrorMessage = "Related entity type is required")]
        public string? RelatedEntityType { get; set; }

        [Required(ErrorMessage = "Related entity ID is required")]
        public Guid RelatedEntityId { get; set; }
        public string? RelatedEntityName { get; set; } // For display purposes

        public string? ReminderSource { get; set; }

        // b. Timing Logic
        [Required(ErrorMessage = "Reminder date/time is required")]
        public DateTime? ReminderDateTime { get; set; }

        public int? ReminderBefore { get; set; }
        public bool RepeatReminder { get; set; }
        public string? RepeatIntervalType { get; set; }
        public int? RepeatCount { get; set; }

        // c. Notification Channels
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public bool SendInAppNotification { get; set; }
        public bool SendPushNotification { get; set; }
        public bool SendWhatsApp { get; set; }

        // d. Target Audience
        [Required(ErrorMessage = "User is required")]
        public Guid UserId { get; set; }
        public string? UserName { get; set; } // For display purposes

        public Guid? CustomerContactId { get; set; }
        public string? CustomerContactName { get; set; } // For display purposes

        public bool IsTeamLevelReminder { get; set; }
        public Guid? SalesTeamId { get; set; }
        public string? SalesTeamName { get; set; } // For display purposes

        // e. Content & Payload
        [Required(ErrorMessage = "Reminder message is required")]
        public string? ReminderMessage { get; set; }

        public string? ReminderNotes { get; set; }
        public string? ReminderLink { get; set; }
        public bool AttachICSFile { get; set; }

        // f. Status & Tracking
        [Required(ErrorMessage = "Reminder status is required")]
        public string? ReminderStatus { get; set; }

        public DateTime? AcknowledgedOn { get; set; }
        public Guid? AcknowledgedBy { get; set; }
        public string? AcknowledgedByName { get; set; } // For display purposes

        public int? RetryCount { get; set; }

        // g. Attachments
        public List<DocumentFileInfo> Attachments { get; set; } = [];

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    // 8. Outcome Log
    public class ST_OutcomeLogViewModel
    {
        // a. Details
        public Guid TaskOutcomeLogId { get; set; }
        public Guid SalesTaskId { get; set; }

        [Required(ErrorMessage = "Outcome type is required")]
        public string? OutcomeType { get; set; }

        [Required(ErrorMessage = "Outcome status is required")]
        public string? OutcomeStatus { get; set; }

        // b. Outcome Timing
        [Required(ErrorMessage = "Outcome date is required")]
        public DateTime? OutcomeDate { get; set; }

        public bool NextActionRequired { get; set; }
        public DateTime? NextFollowUpDueDate { get; set; }

        // c. Insightful Details
        [Required(ErrorMessage = "Engagement summary is required")]
        public string? EngagementSummary { get; set; }

        public string? DetailedOutcomeNotes { get; set; }
        public string? CustomerReaction { get; set; }
        public string? SalesRepSentiment { get; set; }

        // d. Attachments
        public List<DocumentFileInfo> Attachments { get; set; } = [];

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    // 9. Activity Audit Trail
    public class ST_ActivityAuditTrailViewModel
    {
        // a. Core Identification
        public Guid SalesActivityAuditTrailId { get; set; }
        public Guid SalesActivityId { get; set; }

        [Required(ErrorMessage = "Change type is required")]
        public string? ChangeType { get; set; }

        // b. Action Info
        [Required(ErrorMessage = "Action performed by user is required")]
        public Guid ActionPerformedByUserId { get; set; }
        public string? ActionPerformedByUserName { get; set; } // For display purposes

        public string? ActionPerformedByRole { get; set; }

        [Required(ErrorMessage = "Action timestamp is required")]
        public DateTime ActionTimestamp { get; set; }

        public string? ActionSource { get; set; }

        // c. Change Tracking Info
        public string? PreviousStatus { get; set; }
        public string? NewStatus { get; set; }
        public Guid? PreviousAssigneeId { get; set; }
        public string? PreviousAssigneeName { get; set; } // For display purposes

        public Guid? NewAssigneeId { get; set; }
        public string? NewAssigneeName { get; set; } // For display purposes

        public DateTime? PreviousDueDate { get; set; }
        public DateTime? NewDueDate { get; set; }

        // d. Metadata & Reasoning
        public string? ActionRemarks { get; set; }
        public string? ChangeSummary { get; set; }
        public bool RequiresFollowUp { get; set; }
        public string? TriggeringEntity { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }

    // 10. Sales Rep Productivity Metrics
    public class ST_SalesRepProductivityMetricsViewModel
    {
        // a. Core Identification
        public Guid SalesRepProductivityId { get; set; }

        [Required(ErrorMessage = "Sales rep user is required")]
        public Guid SalesRepUserId { get; set; }
        public string? SalesRepUserName { get; set; } // For display purposes

        [Required(ErrorMessage = "Evaluation date is required")]
        public DateTime? EvaluationDate { get; set; }

        [Required(ErrorMessage = "Evaluation period is required")]
        public string? EvaluationPeriod { get; set; }

        // b. Core Metrics Logged
        public int TotalTasksAssigned { get; set; }
        public int TasksCompleted { get; set; }
        public decimal OnTimeCompletionRate { get; set; }
        public int OverdueTasks { get; set; }
        public int AvgTaskCompletionTimeInMinutes { get; set; }
        public int MeetingsHeld { get; set; }
        public int FollowUpsConducted { get; set; }

        // c. Outcome & Conversion Indicators
        public int LeadsConverted { get; set; }
        public int OpportunitiesWon { get; set; }
        public decimal RevenueGenerated { get; set; }
        public decimal ConversionRate { get; set; }
        public int LeadTouchpointCount { get; set; }
        public decimal EngagementSuccessRate { get; set; }

        // System fields
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    // Document File Info
    public class DocumentFileInfo
    {
        public string? FileName { get; set; }
        public long FileSize { get; set; }
        public string? ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string? UploadedBy { get; set; }

        // Additional helpers
        public string? FileExtension => System.IO.Path.GetExtension(FileName)?.ToLowerInvariant();
    }
}