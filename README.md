# Using XPO with sqlite database in blazor web assembly 


Steve Sanderson talk about net 6
https://www.youtube.com/watch?v=kesUNeBZ1Os

C based sqlite to compile

https://github.com/clibs/sqlite

How to compile sqlite in linux or WSL (Windows subsystem for linux)

```<language>
sudo apt-get install cmake default-jre git-core unzip

git clone https://github.com/emscripten-core/emsdk.git
cd emsdk
./emsdk install latest
./emsdk activate latest
source ./emsdk_env.sh
```

SQLite database location in the browser
https://stackoverflow.com/questions/8936878/where-does-chrome-save-its-sqlite-database-to


Command to compile sqlite as a web assemly reference

```<language>
emcc sqlite3.h -shared -o e_sqlite3.o
```
