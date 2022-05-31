using System.Collections.Generic;

namespace iqraProject.DTOs
{
    public class WordDto
    {
        public int Id { get; set; }
        public string WordName { get; set; }
        public string Symbol { get; set; }
        public string Sound { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string Audio { get; set; }
    }

    public class AddWordRequestModel
    {
        public string WordName { get; set; }
        public string Symbol { get; set; }
        public string Sound { get; set; }
        public int LessonId { get; set; }
        public string Audio { get; set; }

    }
    public class UpdateWordRequestModel
    {
        public string WordName { get; set; }
        public string Symbol { get; set; }
        public string Sound { get; set; }
    }
}