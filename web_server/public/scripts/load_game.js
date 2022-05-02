
const loadGame = () => {

   
    let gameInstance = null;

    var container = document.querySelector("#unity-container");
    var canvas = document.querySelector("#unity-canvas");
    var loadingBar = document.querySelector("#unity-loading-bar");
    var progressBarFull = document.querySelector("#unity-progress-bar-full");
    var fullscreenButton = document.querySelector("#unity-fullscreen-button");
    var warningBanner = document.querySelector("#unity-warning");

    // Shows a temporary message banner/ribbon for a few seconds, or
    // a permanent error message on top of the canvas if type=='error'.
    // If type=='warning', a yellow highlight color is used.
    // Modify or remove this function to customize the visually presented
    // way that non-critical warnings and error messages are presented to the
    // user.
    function unityShowBanner(msg, type) {
    function updateBannerVisibility() {
        warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
    }
    var div = document.createElement('div');
    div.innerHTML = msg;
    warningBanner.appendChild(div);
    if (type == 'error') div.style = 'background: red; padding: 10px;';
    else {
        if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
        setTimeout(function() {
        warningBanner.removeChild(div);
        updateBannerVisibility();
        }, 5000);
    }
    updateBannerVisibility();
    }

    var buildUrl = "../tempo_artist/Build";
    var loaderUrl = buildUrl + "/tempo_artist.loader.js";
    var config = {
    dataUrl: buildUrl + "/tempo_artist.data",
    frameworkUrl: buildUrl + "/tempo_artist.framework.js",
    codeUrl: buildUrl + "/tempo_artist.wasm",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "DefaultCompany",
    productName: "pas_game",
    productVersion: "1.0",
    showBanner: unityShowBanner,
    };

    // By default Unity keeps WebGL canvas render target size matched with
    // the DOM size of the canvas element (scaled by window.devicePixelRatio)
    // Set this to false if you want to decouple this synchronization from
    // happening inside the engine, and you would instead like to size up
    // the canvas DOM size and WebGL render target sizes yourself.
    // config.matchWebGLToCanvasSize = false;

    if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
    container.className = "unity-mobile";
    // Avoid draining fillrate performance on mobile devices,
    // and default/override low DPI mode on mobile browsers.
    config.devicePixelRatio = 1;
    unityShowBanner('WebGL builds are not supported on mobile devices.');
    } else {
    canvas.style.width = "1100px";
    canvas.style.height = "560px";
    }
    loadingBar.style.display = "block";

    var script = document.createElement("script");
    script.src = loaderUrl;
    script.onload = () => {
        createUnityInstance(canvas, config, (progress) => {
            progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {


            gameInstance = unityInstance;
            loadingBar.style.display = "none";
            fullscreenButton.onclick = () => {
            unityInstance.SetFullscreen(1);
            };
        }).catch((message) => {
            alert(message);
        });

    };


    function sendIdData(){

        //Obtener el id del local storage
        let id = localStorage.getItem('id');
        gameInstance.SendMessage('ResultsManager', 'sendSaveRequest', parseInt(id));
        console.log("SENDED ID: " + id);

    }

    setInterval(() => {
        sendIdData();
    }, 2000);



    document.body.appendChild(script);


}

