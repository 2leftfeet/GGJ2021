 
Shader "ZIgnore/ZWrite"{
  SubShader{
        // Write Z buffer information after all other geometry, so that the Z buffer contains informations that will allow the Z ignoring objects to be written on top of the world
        Tags{ "Queue" = "Geometry+1" }

        ColorMask 0
        ZTest Always
        ZWrite On

        Pass{ }
    }
}