# YourProjectTitle
TODO: One paragraph description of your overall project

TODO: Other overall project details - added in as you learn about them in class

## Git Notes

To clone this repository for work at home / on another computer:
* If not there already go to [amctavish.visualstudio.com](https://amctavish.visualstudio.com)
* Click on the repos button to the left ![button image](http://staffweb.nsd.org/amctavish/images/DevOpsReposButton.JPG)
* Click **Clone** in the top right, and in the drop-down, select / click on
  **Clone in Visual Studio** under IDE
* You can use the default suggested location, or select another one as
  demonstrated at school.
* Open up the .sln, build it, and you're good to go

Once you've cloned the repository, your work pattern any time you switch
work locations will be:
* Open .sln
* Team Explorer / Branches -> double-click on your feature branch to select it
  (the first time, you'll find it under "remotes/origin")
* Team Explorer / Sync -> Fetch, and pull any incoming commits
* Do work.
* Before leaving, **commit** and **push** all changes.

The sequence "arrive/pull/work/commit/push/leave" is crucial to working smoothly
between multiple locations. You absolutely want to avoid the situation of having
to merge work from one location with work from another.

### Important notes for GUI/Unity person

* Your Unity project lives in the folder of that name within the main folder
* When you create new folders in your project (e.g. art, prefabs), populate each
  with at least a deleteme.txt file before making a commit. Long story short, empty
  folders in a Unity project committed to source control will cause headaches, so
  don't have truly empty folders.
  (_See Unity Project\Assets\Resources\Libraries for an example_).
  You will of course get rid of deleteme.txt files once folders have real contents.
* Always double-check the list of files/changes being committed before you
  actually commit. Have a mentor or teacher by your side for the first few commits.
* If you ever see an error "...WumpusEngine ... could not be found" in Unity,
  that means you need to build the WumpusEngine project - which will copy the dll
  and supporting files to the proper location within your Unity project when done.

## Working with Markdown syntax

A lot of the syntax for these documents is pretty straightforward; some of the
apps you use (e.g. Slack, Teams) likely accept syntax like this to produce
formatted text. Markdown is used throughout this document;
here are some common examples:
* an asterisk at the start of a line creates a bulleted list point
* two asterisks before and after text **makes the text bold**
* an underscore before and after text _makes the text italic_
* use #, ##, ###, etc. at the start of a line to give different heading levels (up to 6)
* [link text](link address) creates a hyperlink - see link below

You can also make tables, insert images, and much more - see
[more detailed guidance for Markdown usage](https://docs.microsoft.com/en-us/azure/devops/project/wiki/markdown-guidance?view=azure-devops)
from Microsoft, geared towards Azure DevOps (VSTS) users.
