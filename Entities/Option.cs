using iqraProject.Enums;

namespace iqraProject.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public string Sound { get; set; }
        public OptionStatus OptionStatus { get; set; }
        public OptionType OptionType { get; set; }
    }
}