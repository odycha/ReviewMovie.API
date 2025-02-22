namespace ReviewMovie.API.Data
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int ReleaseDate { get; set; }
		public string Genre { get; set; } = string.Empty;

		// Navigation Property - One movie can have multiple reviews
		public virtual IList<Review>? Reviews { get; set; }

	}
}
//Why use virtual IList
//Pros:
//Supports Lazy Loading (if enabled in EF Core).
//More flexible: IList<T> allows different list implementations (e.g., List<T>, HashSet<T>, etc.).
//❌ Cons:
//Requires lazy loading to be explicitly enabled in EF Core.
//If lazy loading is not needed, List<T> might be simpler.Pros:

//What is Lazy Loading ?
//Lazy loading means EF Core loads the Reviews only when accessed, instead of fetching them immediately when retrieving a Movie entity.