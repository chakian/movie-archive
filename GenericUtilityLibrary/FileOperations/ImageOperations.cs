using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace com.cagdaskorkut.utility {
	public static class ImageOperations {
		private static double GetRatio ( int width, int height ) {
			return ( Convert.ToDouble ( width ) / Convert.ToDouble ( height ) );
		}

		public static Bitmap ResizeImage ( Bitmap image, int newWidth, int newHeight, bool saveRatio ) {
			if ( image.Width > newWidth && image.Height > newHeight ) {
				Bitmap temp = image;

				if ( saveRatio ) {
					if ( newHeight == 0 || ( Convert.ToDouble ( temp.Width ) / Convert.ToDouble ( temp.Height ) ) > ( Convert.ToDouble ( newWidth ) / Convert.ToDouble ( newHeight ) ) ) {
						newHeight = Convert.ToInt32 ( ( Convert.ToDouble ( newWidth ) / Convert.ToDouble ( temp.Width ) ) * temp.Height );
					} else {
						newWidth = Convert.ToInt32 ( ( Convert.ToDouble ( newHeight ) / Convert.ToDouble ( temp.Height ) ) * temp.Width );
					}
				}

				Bitmap bmap = new Bitmap ( newWidth, newHeight, temp.PixelFormat );
				bmap = new Bitmap ( temp, newWidth, newHeight );

				return bmap;
			} else
				return image;
		}
		public static Bitmap ResizeImage ( Bitmap image, int newWidth, int newHeight ) {
			return ResizeImage ( image, newWidth, newHeight, false );
		}

		public static Bitmap CropImage ( Bitmap image, int x1, int y1, int x2, int y2 ) {
			Bitmap temp = image;
			//specify the crop area
			Rectangle cropArea = new Rectangle ( x1, y1, x2 - x1, y2 - y1 );
			//crop the image
			Bitmap bmap = temp.Clone ( cropArea, temp.PixelFormat );
			//return cropped image
			return bmap;
		}

		public static Bitmap CropAndResizeImage ( Bitmap image, int x1, int y1, int x2, int y2, int width, int height ) {
			//if the image size is already smaller than the requested size.. just skip
			if ( image.Width > width && image.Height > height ) {
				//specify the crop area
				Rectangle cropArea = new Rectangle ( x1, y1, x2 - x1, y2 - y1 );
				//crop the image
				image = image.Clone ( cropArea, image.PixelFormat );
				//resize the image
				image = ResizeImage ( image, width, height, true );
			}
			//return cropped and resized image
			return image;
		}

		public static void CreateDifferentSizesOfTempImage ( String fileName, String imagePath, String smallImagePath, String bigImagePath, Int32[] smallDimensions, Int32[] bigDimensions ) {
			//get image from disk
			//Bitmap temp = (Bitmap)Bitmap.FromFile(imagePath + fileName);
			Bitmap temp = ( Bitmap ) Bitmap.FromFile ( imagePath + fileName );
			//save big version
			Image bigImage = ( Image ) ResizeImage ( temp, bigDimensions[0], bigDimensions[1], true );
			FileSystemOperations.CreateDirectory ( bigImagePath );
			bigImage.Save ( bigImagePath + fileName );
			//save small version
			Image smallImage = ( Image ) ResizeImage ( ( Bitmap ) bigImage, smallDimensions[0], smallDimensions[1], true );
			FileSystemOperations.CreateDirectory ( smallImagePath );
			smallImage.Save ( smallImagePath + fileName );
			smallImage.Dispose ( );
			bigImage.Dispose ( );
		}
	}
}