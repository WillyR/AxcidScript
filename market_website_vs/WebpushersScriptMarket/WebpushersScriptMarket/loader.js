var PusherScript = PusherScript || {};

// The core script loader. Loads packed micro web applications and injects them into the page DOM.
(function(ns){

    ns.showAlert = function(message) {

    };

    ns.installButtonClicked = function(scriptID, el) {
        $(el).button('loading');
    };

    // Register the class
    ns.ScriptLoader = (function() {
        var API_ROOT = "";
        // The actual loader class
        var Loader = function(ns) {
            // Constructor Code
            this.loadRemotePackage("market");
        };

        // Load a package from a remote URL (This is where as a production app, we will need to serve resources over HTTPS and make sure scripts are authorized to run.
        // Serve the result over JSONP
        Loader.prototype.loadRemotePackage = function(package_name) {
			var script = document.createElement( 'script' );
			script.type = 'text/javascript';
			script.src = "http://localhost:8305/Packager.aspx?repackage=" + escape(package_name);
			document.body.appendChild( script );
        };

		/* todo- load package from localstorage or cache) */
        Loader.prototype.loadLocalPackages = function() {

        };
		
		/* load package we found */
        Loader.prototype.processPackage = function(package) {

        };
		
		/* Load a packaged application into the current window */
		Loader.prototype.inject = function(packaged_json)
		{
			// inject JS
			console.log(packaged_json);
		};

		/* inject package contents */
        Loader.prototype.injectJS = function(js) {
			
        };

        Loader.prototype.loadSiteApplications = function() {
            $.getJSON(API_ROOT + 'api/motherfuckinglist', function(data) {
				
            });
        };

        Loader.prototype.loadTopApplications = function() {

        };

        Loader.prototype.renderApplicationsList = function() {

        };

        Loader.prototype.saveManifestWithKey = function(manifest, key) {
            //save our json with auth token + script id?
            if(this.hasLocalStorageAccess())
            {
                $.jStorage.set(key, manifest);
            }
            else
            {
                console.log('Unable to save manifest!');
            }
        };

        Loader.prototype.hasLocalStorageAccess = function() {
            return $.jStorage.storageAvailable();
        };

        // Initialize it so this function returns an instance of itself.
        return new Loader(ns);
    })();

})(PusherScript);

function market_load(obj) { console.log(obj); }