# C# 通过GPIB总线控制Keysight设备

目前决定先通过命令行形式进行控制，实现第一个demo。

此处把lab139中的那台主机称为“主机”。

----

2020/11/11，主机已经插上小米无线网卡，可以连接校园网了，尽管是暂时的。

这样我们可以通过ToDesk进行远程操控（不用TeamViewer了，麻烦）

----

这台主机似乎还未能连上任何一台仪器。换言之，之后至少得找一台机器直连到主机上进行测试。这件事应该可以找zp解决。

【机器直连主机 和 大批机器通过网络连接到主机是不一样的。目前先实现直连的demo吧。】

----

已经在自己的Mac上安装了NI-VISA。可能主机上也需要安装。NI-VISA只支持Windows和Mac。

目前的工作还停留在寻找VISA库以及它的手册的阶段。

[找到了](https://www.ni.com/documentation/zhs/ni-visa/17.0/manual/manual-overview/)。这个页面好像是以html的形式展示帮助信息。

下载VISA手册用到了wget命令。如果想知道的话。

----

看到一篇“混用KEYSIGHT-VISA,NI-VISA所引起的程序启动不正常”的博客，意识到可能Keysight也有自己的visa库？如果有的话值得去找一找。最好不要混用两家公司的库吧。

keysight确实有它自己的visa库，虽然不及NI。似乎这个visa库包含在keysight IO suite中，但我不太能找到它。