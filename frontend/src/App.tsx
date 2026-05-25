import Home from "./pages/Home"
import TodoComponent from "./components/TodoComponent"
import { Routes, Route } from "react-router-dom";
import HomeBootstrap from "./pages/HomeBootstrap";
import TodoBootstrapComponent from "./components/TodoBootstrapComponent";

const App = () => {

  return (
    <>
      <Routes>
        <Route path="/todo" element={<TodoComponent />} />
        <Route path="/bootstrap-todo" element={<TodoBootstrapComponent />} />
        <Route path="/" element={<Home />} />
        <Route path="/bootstrap-home" element={<HomeBootstrap />} /> 
      </Routes>
    </>
  )
}

export default App