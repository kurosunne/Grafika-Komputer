function GLInstance(canvasID){
    var canvas = document.getElementById(canvasID);
    gl = canvas.getContext("webgl2");

    if(!gl) {
        console.error("WebGL context is not available");
        return null;
    }

    gl.clearColor(1.0, 1.0, 1.0, 1.0);

    gl.fClear = function () { 
        this.clear(this.COLOR_BUFFER_BIT | this.DEPTH_BUFFER_BIT);
        return this;
    }

    gl.fSetSize = function(width, height){
        this.canvas.style.width = `${width}px`;
        this.canvas.style.height = `${height}px`;
        this.canvas.width = width;
        this.canvas.height = height;

        this.viewport(0,0,width,height);
        return this;
    }
    return gl;
}