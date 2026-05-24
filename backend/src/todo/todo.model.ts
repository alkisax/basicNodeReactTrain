import mongoose from "mongoose";
import type { Todo } from './todo.types'

const Schema = mongoose.Schema;

const todoSchema = new Schema(
  {
    todo: { type: String },
    user: { type: String },
    completed: { 
      type: Boolean,
      default: false
    },
  },
  {
    collection: 'todos',
    timestamps: true,
  }
)

export default mongoose.model<Todo>('Todo', todoSchema)