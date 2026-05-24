import { z } from 'zod';

export const createTodoSchema = z.object({
  todo: z.string().min(1).max(300),
  user: z.string().max(100).optional().default(''),
  completed: z.boolean().default(false),
});

export const updateTodoSchema = z.object({
  todo: z.string().min(1).max(300).optional(),
  user: z.string().max(100).default('').optional(),
  completed: z.boolean().optional(),
});

export type CreateTodoInput = z.infer<typeof createTodoSchema>;
export type UpdateTodoInput = z.infer<typeof updateTodoSchema>;