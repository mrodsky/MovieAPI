namespace API
{
    public class MovieModel
    {
        public Int32? Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Tags      { get; set; }
        public string Release { get; set; }
        public string Director{ get; set; }
        public string Studio { get; set; }

        public Int32 Runtime { get; set; }

    }
}
