import Home from "./pages/Home"
import TodoComponent from "./components/TodoComponent"
import { Routes, Route } from "react-router-dom";

const App = () => {

  return (
    <>
      <Routes>
        <Route path="/todo" element={<TodoComponent />} />
        <Route path="/" element={<Home />} />
      </Routes>
    </>
  )
}

export default App