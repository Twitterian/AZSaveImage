// By - @Lolicon_sagi
// Last Update : 2015-02-18

// Ctrl + S : ������ ��ġ�� �̹��� ����
// Ctrl + Shift + S : �ٸ� �̸����� ����

FileSystem.privateStore.write('location.dat', System.applicationPath.replace(/[^(.)^(\\)]+(.)exe/, ''), 3);

function SaveImage(id) {
    if (FileSystem.privateStore.exists('location.dat')) {
        var path = FileSystem.privateStore.read('location.dat') + 'Scripts/SaveImage.js.Private/AZSaveImage.exe';
        var urls = TwitterService.call('statuses/lookup.json?id=20,' + id).replace(/\\/g, '');
        System.launchApplication(path, urls + " 0", 1);
    }
}

function SaveImageA(id) {
    if (FileSystem.privateStore.exists('location.dat')) {
        var path = FileSystem.privateStore.read('location.dat') + 'Scripts/SaveImage.js.Private/AZSaveImage.exe';
        var urls = TwitterService.call('statuses/lookup.json?id=20,' + id).replace(/\\/g, '');
        System.launchApplication(path, urls + " 1", 1);
    }
}

System.addKeyBindingHandler('S'.charCodeAt(0), 2, SaveImage);
System.addKeyBindingHandler('S'.charCodeAt(0), 3, SaveImageA);