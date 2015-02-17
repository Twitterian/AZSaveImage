// By - @Lolicon_sagi
// Last Update : 2015-02-18
function SaveImage(id) {
    var urls = TwitterService.call('statuses/lookup.json?id=20,' + id).replace(/\\/g, '');
    System.launchApplication('AZSaveImage.exe', urls, 1);
}
System.addKeyBindingHandler('S'.charCodeAt(0), 2, SaveImage);