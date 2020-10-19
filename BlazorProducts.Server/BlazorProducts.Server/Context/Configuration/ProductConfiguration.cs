using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Context.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasData
			(
				//Mugs
				new Product
				{
					Id = new Guid("0102F709-1DD7-40DE-AF3D-23598C6BBD1F"),
					Name = "Travel Mug",
					Description = @$"Features:
- Features wraparound prints
- Top rack dishwasher safe
- Insulated stainless steel with removable lid
- Mug holds 15oz(443ml)",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1062161969.4889/mug,travel,x1000,center-pad,1000x1000,f8f8f8.u2.jpg",
					Price = 11
				},
				new Product
				{
					Id = new Guid("AC7DE2DC-049C-4328-AB06-6CDE3EBE8AA7"),
					Name = "Classic Mug",
					Description = @"Features
- Features wraparound prints
- Dishwasher safe
- Made from ceramic
- Mug holds 11oz (325ml)",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1063377597.4889/ur,mug_lifestyle,square,1000x1000.u2.jpg",
					Price = 22
				},
				//Clothing
				new Product
				{
					Id = new Guid("D26384CB-64B9-4ACA-ACB0-4EBB8FC53BA2"),
					Name = "Code Maze Logo T-Shirt",
					Description = @"Features
- The standard, traditional t-shirt for everyday wear
- Classic, generous, boxy fit
- Heavyweight 5.3 oz / 180 gsm fabric, solid colors are 100% preshrunk cotton, heather grey is 90% cotton/10% polyester, denim heather is 50% cotton/ 50% polyester
- Double-needle hems and neck band for durability",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1063364659.4889/ra,vneck,x1900,101010:01c5ca27c6,front-c,160,70,1000,1000-bg,f8f8f8.u2.jpg",
					Price = 20
				},
				new Product
				{
					Id = new Guid("B47D4C3C-3E29-49B9-B6BE-28E5EE4625BE"),
					Name = "Pullover Hoodie",
					Description = @"Features
- Heavyweight 9oz preshrunk cotton rich fleece made from 80% Cotton, 20% Polyester
- Front pouch pocket, matching drawstring and rib cuffs
- Ethically sourced following the World Responsible Apparel Practices Standards
- Note: If you like your hoodies baggy go 2 sizes up",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1063364659.4889/ssrco,mhoodie,mens,101010:01c5ca27c6,front,square_three_quarter,x1000-bg,f8f8f8.1u2.jpg",
					Price = 30
				},
				new Product
				{
					Id = new Guid("54B2F952-B63E-4CAD-8B38-C09955FE4C62"),
					Name = "Fitted Scoop T-Shirt",
					Description = @"Features
- Scoop neck, cap sleeves, and fitted cut add up to a fashionably casual tee
- Slim fit, so consider going a size up if that's not your thing
- Model shown is 5'11"" / 180 cm tall and wearing size Medium
- Solid colors are 100 % cotton; heathered fabrics are 90 % cotton, 10 % polyester
- Cold wash and hang dry to preserve your print",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1063364659.4889/ssrco,mhoodiez,mens,101010:01c5ca27c6,front,square_three_quarter,1000x1000-bg,f8f8f8.u2.jpg",
					Price = 40
				},
				new Product
				{
					Id = new Guid("83E0AA87-158F-4E5F-A8F7-E5A98D13E3A5"),
					Name = "Zipped Hoodie",
					Description = @"Features
- Heavyweight 9 oz preshrunk cotton rich fleece 80% cotton, 20% polyester
- Front pouch pocket, matching drawstring and rib cuffs
- Ethically sourced following the World Responsible Apparel Practices Standards
- Note: If you like your hoodies baggy go 2 sizes up",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1063364659.4889/ra,fitted_scoop,x2000,101010:01c5ca27c6,front-c,160,143,1000,1000-bg,f8f8f8.u2.jpg",
					Price = 55
				},
				//Phone
				new Product
				{
					Id = new Guid("488AAA0E-AA7E-4820-B4E9-5715F0E5186E"),
					Name = "iPhone Case & Cover",
					Description = @"Features
- Double layer clip-on protective case with extra durability
- Impact resistant polycarbonate shell and shock absorbing inner TPU liner
- Super-bright colors embedded directly into the case
- Secure fit with design wrapping around side of the case and full access to ports
- Compatible with Qi-standard wireless charging
- Thickness 1/8 inch (3mm), weight 30g",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1062161956.4889/icr,iphone_11_soft,back,a,x1000-pad,1000x1000,f8f8f8.u2.jpg",
					Price = 25
				},
				new Product
				{
					Id = new Guid("4E693871-788D-4DB4-89E5-DD7678DB975E"),
					Name = "Case & Skin for Samsung Galaxy",
					Description = @"Features
- Double layer clip-on protective case with extra durability
- Impact resistant polycarbonate shell and shock absorbing liner
- Super-bright colors embedded directly into the case
- Secure fit with design wrapping around side of the case and full access to ports
- Compatible with Qi-standard wireless charging
- Thickness 1/8 inch (3mm), weight 30g",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1062161956.4889/icr,samsung_galaxy_s10_snap,back,a,x1000-pad,1000x1000,f8f8f8.1u2.jpg",
					Price = 35
				},
				new Product
				{
					Id = new Guid("2D3C2ABE-85EC-4D1E-9FEF-9B4BFEA5F459"),
					Name = "iPad Case & Skin",
					Description = @"Features
- Form fitting removable vinyl decal with laminate top coat
- Scratch resistant backing
- 3M Controltac with air bubble grooves for easy, bubble-free placement
- Compatible with Qi-standard wireless charging
- Weight <5g
- Thickness <1/32 inch (<1mm)",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1063329780.4889/mwo,x1000,ipad_2_snap-pad,1000x1000,f8f8f8.u2.jpg",
					Price = 45
				},
				//Home
				new Product
				{
					Id = new Guid("D1F22836-6342-480A-BE2F-035EEB010FD0"),
					Name = "Wall Clock",
					Description = @"Features
- Modern printed polypropylene with plexiglass face
- Bamboo wood frame with natural finish or painted black or white
- 4 customizable metal hand colors to choose from
- Quartz clock mechanism(AA battery not included)
- Built in hook at back for easy hanging",
					Supplier = "Code Maze",
					ImageUrl = "https://ih1.redbubble.net/image.1062161997.4889/clkc,bamboo,white,1000x1000-bg,f8f8f8.u2.jpg",
					Price = 25
				}
			);
		}
	}
}
