using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace webpushers
{
	/// <summary>
	/// A micro application that packages itself.
	/// </summary>
	public class MicroApp
	{
		protected string base_directory;
		
		public MicroApp(string base_directory)
		{
			if (!base_directory.EndsWith(Path.DirectorySeparatorChar+"")) base_directory += Path.DirectorySeparatorChar + "";	
			this.base_directory = base_directory;
			
			// Quickly figure out a manifest for the files in the directory
			string manifest_path = (from file in Directory.EnumerateFiles(base_directory) select file).Where((f) => f.Contains("manifest.json")).FirstOrDefault();

			// Load in the application manifest
			if (manifest_path != null)
			{
				// load the manifest file into a new micro app
				JObject manifest = JsonConvert.DeserializeObject(File.ReadAllText(manifest_path)) as JObject;

				// the base manifest settings
				MicroAppManifest new_manifest = new MicroAppManifest() {
					ApplicationName = manifest["app_name"].ToString(),
					AppIcon = manifest["app_icon"].ToString()
				};

				// set the resource file paths
				new_manifest.css_files = (from f in manifest["css_files"] select f).Select((j) => j.ToString());
				new_manifest.script_files = (from f in manifest["scripts"] select f).Select((j) => j.ToString());

				BundledMicroApplicationPart app = Bundle(manifest);

				dynamic package_output = new
				{
					Manifest = new_manifest,
					Parts = new List<BundledMicroApplicationPart>()
				};

				// widgets
				foreach (JObject widget in manifest["widgets"])
				{
					BundledMicroApplicationPart w = Bundle(widget);
					package_output.Parts.Add(w);
				}
				
				package_output.Parts.Add(app);
				
				string json = JsonConvert.SerializeObject(package_output);
				JSONOutput = json;
			}
			else
			{
				Console.WriteLine("Manifest could not be loaded from directory "+base_directory + "!");
				Environment.Exit(0);
			}
		}

		private BundledMicroApplicationPart Bundle(JObject part)
		{
			string[] part_css_files = (from f in part["css_files"] select f).Select((j) => j.ToString()).ToArray();
			string[] part_script_files = (from f in part["scripts"] select f).Select((j) => j.ToString()).ToArray();

			// create the instance that will store the bundled app
			BundledMicroApplicationPart app = new BundledMicroApplicationPart();

			if (part["package_name"] != null) app.Name = part["package_name"].ToString();
			if (part["frame_id"] != null) app.Name = part["frame_id"].ToString();

			if (part["to"] != null) app.To = part["to"].ToString();

			// compress the CSS
			Yahoo.Yui.Compressor.CssCompressor css_compressor = new Yahoo.Yui.Compressor.CssCompressor();

			List<string> css_files = new List<string>();
			List<string> js_files = new List<string>();

			foreach (string script in part_script_files)
			{
				js_files.Add(File.ReadAllText(base_directory + script));
			}
				
			foreach (string stylesheet in part_css_files)
			{
				css_files.Add(css_compressor.Compress(File.ReadAllText(base_directory + stylesheet)));
			}

			js_files = js_files.Select((f) => PackerUtilities.StringToBase64Encoded(f)).ToList();
			css_files = css_files.Select((f) => PackerUtilities.StringToBase64Encoded(f)).ToList();

			// add the bundled app
			app.Scripts = js_files.ToArray();
			app.CSS = css_files.ToArray();

			return app;
		}

		public string JSONOutput
		{
			get;
			set;
		}

		internal class BundledMicroApplicationPart
		{
			public string Name { get; set; }
			public string To { get; set; }
			public String[] Scripts { get; set; }
			public String[] CSS { get; set; }
		}

		sealed internal class MicroAppManifest
		{
			// these should all contain the relative paths to the various files making up the microapplication.
			public IEnumerable<string> css_files { get; set; }
			public IEnumerable<string> script_files { get; set; }
			public IEnumerable<string> resource_files { get; set; }

			/// <summary>
			/// Json Manifest
			/// </summary>
			public JObject Manifest { get; set; }

			/// <summary>
			/// Application display name
			/// </summary>
			public string ApplicationName { get; set; }
			public string AppIcon { get; set; }
		}
	}
}
