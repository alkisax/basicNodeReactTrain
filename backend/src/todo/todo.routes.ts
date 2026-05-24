import { Router } from "express";
import { todoController } from "./todo.controllers";

const router = Router();

router.get('/', todoController.readAll)
router.get('/user-todos/:user', todoController.readUserTodos)
router.get('/:id', todoController.readTodoById)
router.post('/create-todo', todoController.create)
router.post('/update-todo/:id', todoController.updateTodoById)
router.post('/delete-todo/:id', todoController.deleteTodoById)
router.post('/delete-all-user-todos/:user', todoController.deleteAllUserTodos)

export default router