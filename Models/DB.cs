using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public int UserId { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    private string _passwordHash = string.Empty;

    // PasswordHash ve Salt alanları özel olarak erişilebilir.
    public string PasswordHash
    {
        get { return _passwordHash; }
        private set { _passwordHash = value; }
    }

    public string Salt { get; private set; } = string.Empty;

    public string Password
    {
        set
        {
            Salt = GenerateSalt(); // Yeni bir salt oluştur.
            PasswordHash = HashPassword(value, Salt); // Şifreyi hash'le ve salt ile birlikte sakla.
        }
    }

    // Şifreyi hash'lemek için bir metot
    public static string HashPassword(string password, string salt)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }

    // Rastgele bir salt oluşturmak için bir metot
    private static string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    // Şifre doğrulama metodu
    public bool VerifyPassword(string password)
    {
        string hash = HashPassword(password, Salt);
        return hash == PasswordHash;
    }

    public ICollection<MovieLike>? MovieLikes { get; set; } = new List<MovieLike>();
    public ICollection<TVShowLike>? TVShowLikes { get; set; } = new List<TVShowLike>();
    public ICollection<MovieDislike>? MovieDislikes { get; set; } = new List<MovieDislike>();
    public ICollection<TVShowDislike>? TVShowDislikes { get; set; } = new List<TVShowDislike>();
    public ICollection<MovieComment>? MovieComments { get; set; } = new List<MovieComment>();
    public ICollection<TVShowComment>? TVShowComments { get; set; } = new List<TVShowComment>();
    public ICollection<MovieWatchList>? MovieWatchLists { get; set; } = new List<MovieWatchList>();
    public ICollection<TVShowWatchList>? TVShowWatchLists { get; set; } = new List<TVShowWatchList>();
}

public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; } = null!;
    public double? Popularity { get; set; }
    public string? Overview { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? PosterPath { get; set; }

    public ICollection<MovieGenre>? MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<MovieLike>? MovieLikes { get; set; } = new List<MovieLike>();
    public ICollection<MovieDislike>? MovieDislikes { get; set; } = new List<MovieDislike>();
    public ICollection<MovieComment>? MovieComments { get; set; } = new List<MovieComment>();
    public ICollection<MovieWatchList>? MovieWatchLists { get; set; } = new List<MovieWatchList>();
}

public class TVShow
{
    public int TVShowId { get; set; }
    public string? Title { get; set; }
    public double? Popularity { get; set; }
    public string? Overview { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? PosterPath { get; set; }

    public ICollection<TVShowGenre>? TVShowGenres { get; set; } = new List<TVShowGenre>();
    public ICollection<TVShowLike>? TVShowLikes { get; set; } = new List<TVShowLike>();
    public ICollection<TVShowDislike>? TVShowDislikes { get; set; } = new List<TVShowDislike>();
    public ICollection<TVShowComment>? TVShowComments { get; set; } = new List<TVShowComment>();
    public ICollection<TVShowWatchList>? TVShowWatchLists { get; set; } = new List<TVShowWatchList>();
}

public class Genre
{
  
    public int GenreId { get; set; }
    public string? Name { get; set; } // Eklenen 'required' ile çözüm


    public ICollection<MovieGenre>? MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<TVShowGenre>? TVShowGenres { get; set; } = new List<TVShowGenre>();
}

// Relationship Entities
public class MovieGenre
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}

public class TVShowGenre
{
    public int TVShowId { get; set; }
    public TVShow TVShow { get; set; } = null!;

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}

public class MovieLike
{
    public int MovieLikeId { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

public class TVShowLike
{
    public int TVShowLikeId { get; set; }
    public int TVShowId { get; set; }
    public TVShow TVShow { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

public class MovieDislike
{
    public int MovieDislikeId { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

public class TVShowDislike
{
    public int TVShowDislikeId { get; set; }
    public int TVShowId { get; set; }
    public TVShow TVShow { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

public class MovieComment
{
    public int MovieCommentId { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Comment { get; set; } = string.Empty;
}

public class TVShowComment
{
    public int TVShowCommentId { get; set; }
    public int TVShowId { get; set; }
    public TVShow TVShow { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Comment { get; set; } = string.Empty;
}

public class MovieWatchList
{
    public int MovieWatchListId { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

public class TVShowWatchList
{
    public int TVShowWatchListId { get; set; }
    public int TVShowId { get; set; }
    public TVShow TVShow { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}