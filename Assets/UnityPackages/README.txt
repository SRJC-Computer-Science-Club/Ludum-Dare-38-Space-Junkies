In case you're wonder what the heck all of this json stuff is, let me do my
best to explain. Json is a data format similar to XML that allows to write
a set of data that we can then to various objects in our game. The main advantage
to it is that it is very easy to read/write and has compatiblity with things like
arrays. Though its main use is for internet related things, I think it would be
pretty niffty to be used as a way to set up our items (or possibly item types).
Now, unity DOES actually have some native json support, but it is very limited in
what it can do. Json.NET is a popular json famework which unity also has some 
compatibility with, but is pretty out dated  and is a bit weird from what I know, 
so the packages that have been imported in this folder are to allow a more 
standerd and modern version of json.net to run on Unity.

(Also, because the packages are based on the .NET framework, I'm not sure what that
will to the portability of our game. .NET has pretty decent compatiblity with macOS and
Linux, but it's worth looking into.)