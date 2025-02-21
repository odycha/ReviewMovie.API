using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReviewMovie.API.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
				new IdentityRole
				{
					Id = "285547ad-94d0-4f44-86a0-00e559d46207",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				},
				new IdentityRole
				{
					Id = "d3e6be60-f4e0-43ef-9408-9cb862cb8665",
					Name = "User",
					NormalizedName = "USER"
				}
			);
		}
	}
}
