# git in the Console

Setup: VSCode and built-in terminal

> ## Tasks
>
> - Create a repository from the command line **Commanding the Console**
>   - `git init`
>   - *ReadMe.md*
>   - `git status`
>   - `git add ReadMe.md`
>   - `git commit`
>   - `git log`
>   - *.gitignore* + *.gitattributes* - content from website
>   - `git add .` & commit
>   - `git remote -v`
>   - Blank repo on GitHub
>   - add remote and push
>   - ReadMe - Tips on commits: small, frequent, good messages
>   - add/commit/status
>   - Edit ReadMe for new section on git commands
>
> ![Commit Messages](https://imgs.xkcd.com/comics/git_commit.png)
>
> ----
>
> ## Readings
>
> - [Version Control in Software Development](https://dmit-2018.github.io/topics/dvcs/)
> - [git Basics](https://dmit-2018.github.io/topics/dvcs/gh4w.html#signing-up-with-github-com)
> - [Resolving Merge Conflicts](https://dmit-2018.github.io/topics/dvcs/conflicts/)

## Git Commands

- `git init` – This command is used to initialize (set up) a git repository in the current directory. When a git repository is set up, there will be a hidden folder named ".git" that contains all the history of commits for the repository.
- `git add .` – This command is used to stage un-tracked and modified files for a commit. If a file is not being tracked (and is not listed in .gitignore), this command will mark the file as a new item for the repository to track. If a file has been modified, then this command will mark it as ready to be committed.
- `git status` – This command is used to give you the status of your code repository.
- `git commit -m "message"` – The commit command is used to save staged files to the repository. When a commit occurs, all of the files staged for committal are compared against the last commit, and the changes between them becomes the new commit. Additionally, the user is identified for the commit (typically by a user name and e-mail) - this is called blame.
- `git pull` – This command is used to get a remote repository and merge it with the local repository. Remote repositories are typically hosted online.
- `git push` – This command is used to take the local repository and send/merge it with a remote repository.
