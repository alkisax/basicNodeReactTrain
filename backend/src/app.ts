import express from 'express';
import type { Request, Response } from 'express';
import cors from 'cors';
import swaggerUi from 'swagger-ui-express';
import todoRoutes from './todo/todo.routes';
import swaggerSpec from './utils/swagger';

const app = express();
app.use(cors());
app.use(express.json());
app.get('/health', (_req:Request, res:Response) => {
  res.status(200).send('health ok')
});
app.get('/api/ping', (_req: Request, res: Response) => {
  console.log("someone pinged here");
  res.status(200).json({message: 'pong'})
});

app.use('/todo', todoRoutes)
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec))

export default app;