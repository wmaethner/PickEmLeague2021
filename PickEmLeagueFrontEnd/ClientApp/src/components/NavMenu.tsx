import React, { Component, useState } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import "../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { useUserContext } from "../Data/Contexts/UserContext";

export function NavMenu() {
  const [collapsed, setCollapse] = useState<boolean>(false);
  const { user } = useUserContext();

  const adminPages = () => {
    return user?.isAdmin ? (
      <ul className="navbar-nav flex-grow">
        <NavItem>
          <NavLink tag={Link} className="text-dark" to="/games">
            Games
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink tag={Link} className="text-dark" to="/users">
            Users
          </NavLink>
        </NavItem>
      </ul>
    ) : (
      ""
    );
  };

  const allUserPages = () => {
    return (
      <ul className="navbar-nav flex-grow">
        <NavItem>
          <NavLink tag={Link} className="text-dark" to="/">
            Home
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink tag={Link} className="text-dark" to="/gamePicks">
            Game Picks
          </NavLink>
        </NavItem>
      </ul>
    );
  };

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        light
      >
        <Container>
          <NavbarBrand tag={Link} to="/">
            PickEmLeague
          </NavbarBrand>
          <NavbarToggler
            onClick={() => setCollapse(!collapsed)}
            className="mr-2"
          />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!collapsed}
            navbar
          >
            {allUserPages()}
            {adminPages()}
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
}
