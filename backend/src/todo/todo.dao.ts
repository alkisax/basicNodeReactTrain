import TodoModel from "./todo.model";
import type { Todo } from "./todo.types";

// create
const create = async (data: Partial<Todo>): Promise<Todo> => {
  try {
    if (!data.todo || !data.todo.trim()) {
      throw new Error("Todo is required");
    }
    const newTodo = new TodoModel(data);
    return await newTodo.save();
  } catch (error: unknown) {
    if (error instanceof Error) {
      throw new Error(`DAO: error creating todo, ${error.message}`);
    }
    throw new Error("Unknown error");
  }
};
// read
// read all
const readAll = async (): Promise<Todo[]> => {
  try {
    return await TodoModel.find().sort({ createdAt: -1 });
  } catch {
    throw new Error("DAO: Failed to fetch todos");
  }
};

// read specific todo
const readTodoById = async (id: string): Promise<Todo | null> => {
  try {
    return await TodoModel.findById(id);
  } catch {
    throw new Error("DAO: Failed to fetch todo");
  }
};

// read user todo
const readUserTodos = async (user: string): Promise<Todo[]> => {
  try {
    return await TodoModel.find({ user });
  } catch {
    throw new Error("DAO: Failed to fetch user todos");
  }
};

// update
const updateTodoById = async (id: string, updates: Partial<Todo>): Promise<Todo | null> => {
  const updated = await TodoModel.findByIdAndUpdate(id, updates, { returnDocument: 'after' })
  if (!updated) {
    throw new Error('DAO: todo was not found to be updated')
  }
  return updated
}

// delete
const deleteTodoById = async (id: string): Promise<Todo | null> => {
  const deleted = await TodoModel.findByIdAndDelete(id);
  if (!deleted) {
    throw new Error("DAO: todo was not found to be deleted");
  }
  return deleted;
};

const deleteAllUserTodos = async (user: string): Promise<boolean> => {
  const deleted = await TodoModel.deleteMany({ user });
  if (deleted.deletedCount === 0) {
    throw new Error("DAO: user has no todos to be deleted");
  }
  return true;
};

export const todoDao = {
  create,
  readAll,
  readTodoById,
  readUserTodos,
  updateTodoById,
  deleteTodoById,
  deleteAllUserTodos,
};
