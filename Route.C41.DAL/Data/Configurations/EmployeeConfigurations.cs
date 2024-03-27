using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Data.Configurations
{
	internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(E => E.Address).IsRequired();

			builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

			builder.Property(E => E.Gender)
					.HasConversion(
					(Gender) => Gender.ToString(),
					(GenderAsString) => (Gender)Enum.Parse(typeof(Gender), GenderAsString)
					);

			builder.Property(E => E.EmployeeType)
					.HasConversion(
					(Type) => Type.ToString(),
					(TypeAsString) => (EmpType)Enum.Parse(typeof(Type), TypeAsString)
					);
		}
	}
}
