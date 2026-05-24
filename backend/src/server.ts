import dotenv from 'dotenv'
import { PORT } from './constants/constants'
import app from './app'
import { connectMongo } from './db/mongo';

dotenv.config()

const start = async () => {
  await connectMongo();
  app.listen(PORT, () => {
    console.log(`http://localhost:${PORT}`);
    console.log(`http://localhost:${PORT}/api-docs`);
  });
};
start();