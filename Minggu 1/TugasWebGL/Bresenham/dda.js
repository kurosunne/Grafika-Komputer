function garisDDA(x1, y1, x2, y2) {
	var c=document.getElementById("myCanvas");
	var ctx=c.getContext("2d");
	var imgData=ctx.createImageData(1,1);
	for (var i = 0; i<imgData.data.length; i+=4) {
		imgData.data[i+0]=0;
		imgData.data[i+1]=100;
		imgData.data[i+2]=50;
		imgData.data[i+3]=255;
	}

    var titikx_awal = x1;
	var titiky_awal = y1;

	var titikx_akhir = x2;
	var titiky_akhir = y2;

	var dx = titikx_akhir-titikx_awal;
	var dy = titiky_akhir-titiky_awal;
    var step = 0;

	if (dx>dy) {
		step=dx
	} else {
		step=dy;
	}

	var x_inc = dx/step;
	var y_inc = dy/step;

	var x = titikx_awal;
	var y = titiky_awal;

	for (var s = 0; s <step; s+=1) {
		x=x+x_inc;
		xa=Math.round(x);
		y=y+y_inc;
		ya=Math.round(y);
		ctx.putImageData(imgData,xa,ya);
		console.log(xa + " " + ya);
	}
}