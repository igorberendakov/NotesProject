namespace NotesApp.Infrastructure.Dtos
{
    public record NotificationViewDto(Guid Id, DateTime TimeBinding)
    {
        public NoteViewDto Note { get; set; } = null!;
    };
    public record NotificationCreateDto(Guid NoteId)
    {
        private DateTime _timeBinding;
        public DateTime TimeBinding
        {
            get { return _timeBinding; }
            init
            {
                if (value.Kind == DateTimeKind.Utc)
                {
                    _timeBinding = value;
                    return;
                }

                _timeBinding = TimeZoneInfo.ConvertTimeToUtc(value);
            }
        }
    };
    public record NotificationUpdateDto(Guid Id)
    {
        private DateTime _timeBinding;
        public DateTime TimeBinding
        {
            get { return _timeBinding; }
            init
            {
                if (value.Kind == DateTimeKind.Utc)
                {
                    _timeBinding = value;
                    return;
                }

                _timeBinding = TimeZoneInfo.ConvertTimeToUtc(value);
            }
        }
    };
}