// By - @Lolicon_sagi
// Last Update : 2015-02-18

// 이미지를 내려받고싶은 트윗을 선택하고 Ctrl + S

function SaveImage(id) {
    if (FileSystem.privateStore.exists('location.dat')) {
        var path = FileSystem.privateStore.read('location.dat');
        var urls = TwitterService.call('statuses/lookup.json?id=20,' + id).replace(/\\/g, '');
        System.launchApplication(path + 'Scripts/SaveImage.js.Private/AZSaveImage.exe', urls, 0);
    }
}

FileSystem.privateStore.write('location.dat', System.applicationPath.replace(/[^(.)^(\\)]+(.)exe/, ''), 3);
System.addKeyBindingHandler('S'.charCodeAt(0), 2, SaveImage);

System.applicationPath.substr(0, System.applicationPath.lastIndexOf("\\")) + "\\Taiga.exe";