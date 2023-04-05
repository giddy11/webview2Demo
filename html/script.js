function sendDataToWpf() {
    window.chrome.webview.postMessage("This is the data.");
}

function SendThisItem(item){
    // console.log(item);
    window.chrome.webview.postMessage(item);
}