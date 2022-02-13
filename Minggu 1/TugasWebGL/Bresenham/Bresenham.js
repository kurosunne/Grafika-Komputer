var ctx = c.getContext('2d');
c.width = innerWidth;
c.height = innerHeight;
onmousemove = function(e){
	e = e || window.event;
	ctx.clearRect(0,0,c.width,c.height);
	brezLine(c.width/2,c.height/2,e.pageX,e.pageY);
}

function brezLine(x1,y1,x2,y2){
x1 |= 0; y1 |= 0; x2 |= 0; y2 |= 0; 
var dx = x2 - x1, dy = y2 - y1; 
var sx = (dx > 0) - (dx < 0), sy = (dy > 0) - (dy < 0); 
dx *= sx; dy *= sy; 

ctx.fillRect(x1, y1, 1, 1); 
if( !(dx || dy) )return;  
var d = 0, x = x1, y = y1, v;
if(dy < dx)
  for(v = 0 | (dy << 15) / dx * sy; x ^ x2; x += sx, d &= 32767) 
	ctx.fillRect(x, y += (d += v) >> 15, 1, 1); 
else 
  for(v = 0 | (dx << 15) / dy * sx; y ^ y2; y += sy, d &= 32767) 
	ctx.fillRect(x += (d += v) >> 15, y, 1, 1); 
};