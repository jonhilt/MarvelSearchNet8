using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarvelSearch.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Favourite> Favourite { get; set; }
}

public class Favourite
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int MarvelId { get; set; }
    public string CharacterName { get; set; }
}
