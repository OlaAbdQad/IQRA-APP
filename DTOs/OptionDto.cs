using iqraProject.Enums;

namespace iqraProject.DTOs
{
    public class OptionDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public string Sound { get; set; }
        public OptionStatus OptionStatus { get; set; }
        public OptionType OptionType { get; set; }
    }

    public class AddOptionRequestModel
    {
        public int QuestionId { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public string Sound { get; set; }
        public OptionStatus OptionStatus { get; set; }
        public OptionType OptionType { get; set; }
    }

    public class UpdateOptionRequestModel
    {
        public string Label { get; set; }
        public string Text { get; set; }
        public string Sound { get; set; }
        public OptionStatus OptionStatus { get; set; }
        public OptionType OptionType { get; set; }
    }
}