import { Request, Response } from "express";
import { todoDao } from "./todo.dao";
import { createTodoSchema, updateTodoSchema } from "./todo.validation";
import { ZodError } from "zod";

// create
const create = async (req: Request, res: Response) => {
  try {
    const data = req.body;
    const parsed = createTodoSchema.parse(data);
    const todo = await todoDao.create(parsed);
    return res.status(201).json({ status: true, data: todo });
  } catch (error) {
    if (error instanceof ZodError) {
      return res.status(400).json({
        status: false,
        message: "Validation failed",
        details: error.issues,
      });
    }
    return res.status(500).json({
      status: false,
      message: "Internal server error",
    });
  }
};

// readAll
const readAll = async (_req: Request, res: Response) => {
  try {
    const todos = await todoDao.readAll();
    return res.status(200).json({ status: true, data: todos });
  } catch {
    return res.status(500).json({
      status: false,
      message: "Failed to fetch todos",
    });
  }
};

// readTodoById
const readTodoById = async (req: Request, res: Response) => {
  try {
    const { id } = req.params;
    if (typeof id !== "string" || !id) {
      return res.status(400).json({
        status: false,
        message: "no valid id provided",
      });
    }

    const todo = await todoDao.readTodoById(id);
    return res.status(200).json({ status: true, data: todo });
  } catch {
    return res.status(500).json({
      status: false,
      message: "Failed to fetch todos",
    });
  }
};

// readUserTodos
const readUserTodos = async (req: Request, res: Response) => {
  try {
    const { user } = req.params;
    if (typeof user !== "string" || !user) {
      return res.status(400).json({
        status: false,
        message: "no valid user provided",
      });
    }

    const todos = await todoDao.readUserTodos(user);
    return res.status(200).json({ status: true, data: todos });
  } catch {
    return res.status(500).json({
      status: false,
      message: "Failed to fetch todos",
    });
  }
};

// updateTodoById
const updateTodoById = async (req: Request, res: Response) => {
  try {
    const { id } = req.params;
    if (typeof id !== "string" || !id) {
      return res.status(400).json({
        status: false,
        message: "no valid id provided",
      });
    }

    const data = req.body;
    const parsedUpdates = updateTodoSchema.parse(data);
    const todo = await todoDao.updateTodoById(id, parsedUpdates);
    return res.status(200).json({ status: true, data: todo });
  } catch (error) {
    if (error instanceof ZodError) {
      return res.status(400).json({
        status: false,
        message: "Validation failed",
        details: error.issues,
      });
    }
    return res.status(500).json({
      status: false,
      message: "Internal server error",
    });
  }
};

// deleteTodoById
const deleteTodoById = async (req: Request, res: Response) => {
  try {
    const { id } = req.params;
    if (typeof id !== "string" || !id) {
      return res.status(400).json({
        status: false,
        message: "no valid id provided",
      });
    }
    const todo = await todoDao.deleteTodoById(id);
    if (!todo) {
      return res.status(404).json({
        status: false,
        message: "Todo not found",
      });
    }
    return res.status(200).json({ status: true, data: todo });
  } catch {
    return res.status(500).json({
      status: false,
      message: "Failed to delete todo",
    });
  }
};

// deleteAllUserTodos
const deleteAllUserTodos = async (req: Request, res: Response) => {
  try {
    const { user } = req.params;
    if (typeof user !== "string" || !user) {
      return res.status(400).json({
        status: false,
        message: "no valid id provided",
      });
    }
    const todo = await todoDao.deleteAllUserTodos(user);
    return res.status(200).json({ status: true, data: todo });
  } catch {
    return res.status(500).json({
      status: false,
      message: "Failed to delete todos",
    });
  }
};

export const todoController = {
  create,
  readAll,
  readTodoById,
  readUserTodos,
  updateTodoById,
  deleteTodoById,
  deleteAllUserTodos,
};
