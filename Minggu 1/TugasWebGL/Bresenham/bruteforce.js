function garisBF(x1, y1, x2, y2) {
	var c=document.getElementById("myCanvas");
	var ctx=c.getContext("2d");
	var imgData=ctx.createImageData(1,1);
	var cwidth = (c.scrollWidth)/2;
	var cheight = (c.scrollHeight)/2;

	
	var titikx_awal = x1;
	var titiky_awal = y1;

	var titikx_akhir = x2;
	var titiky_akhir = y2;	

for (var i=0;i<imgData.data.length;i+=4)
  {
	  //warna hijau
  imgData.data[i+0]=0;
  imgData.data[i+1]=100;
  imgData.data[i+2]=50;
  imgData.data[i+3]=255;
  }
  
  
  
  if (titikx_awal==titikx_akhir)
	{
		for (var y=titiky_awal;y<titiky_akhir;y+=1)
		{
		x=titikx_awal;
		ctx.putImageData(imgData,x,y);
		}
	}
  else
	if (titiky_awal==titiky_akhir)
	{
		for (var x=titikx_awal;x<titikx_akhir;x+=1)
		{
		y=titiky_awal;
		ctx.putImageData(imgData,x,y);
		}
	}
	else
	{
			
		var deltax =  titikx_akhir - titikx_awal;
		var deltay = titiky_akhir - titiky_awal;
		if (titikx_awal>titikx_akhir)
		{
		var m= deltay/deltax*-1;
		var n = (titikx_akhir - titikx_awal +1)*-1;
		}else
		{
		var m= deltay/deltax;
		var n = (titikx_akhir - titikx_awal +1);
		}	

		if (titikx_awal>titikx_akhir)
		{
			for (var x=titikx_awal;x>=titikx_akhir;x-=1)
			{
			y = (m*(titikx_awal-x))+titiky_awal;
			ctx.putImageData(imgData,x,y);
			}
		}
		else
		{
			for (var x=titikx_awal;x<=titikx_akhir;x+=1)
			{
			y = (m*(x-titikx_awal))+titiky_awal;
			ctx.putImageData(imgData,x,y);
			}
		}
	}
  
  
  
}