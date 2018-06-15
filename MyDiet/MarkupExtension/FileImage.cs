using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDiet
{
	[ContentProperty("ResourceId")]
	public class FileImage : IMarkupExtension
	{
		public string ResourceId { get; set; }
		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (String.IsNullOrWhiteSpace(ResourceId))
				return null;
			return ImageSource.FromFile(ResourceId);
		}
	}
}
