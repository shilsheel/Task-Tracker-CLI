# Task-Tracker-CLI
A simple command-line application for managing tasks efficiently.

Features

✔ Add tasks with descriptions and statuses

✔ Update task descriptions and statuses

✔ Delete tasks

✔ Mark tasks as In Progress or Done

✔ List all tasks or filter by status

Usage:

Add a new task
task-cli add "Buy groceries" "Purchase milk, eggs, and bread" "todo"


Update a task:

task-cli update 1 "Buy groceries and cook dinner" "in-progress"

Delete a task:

task-cli delete 1

Mark as In Progress / Done / Not Done:
task-cli mark-in-progress 1
task-cli mark-done 1
task-cli mark-not-done 1

List tasks:

task-cli list        # Show all tasks
task-cli list todo   # Show only pending tasks
task-cli list done   # Show completed tasks

Setup & Run:

1.Clone the repository
2..Navigate to the project folder
3.Build and run the project

dotnet build

dotnet run -- add "Example Task" "Example Description" "todo"

License

This project is licensed under the MIT License.
