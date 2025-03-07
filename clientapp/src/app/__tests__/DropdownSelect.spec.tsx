import "@testing-library/jest-dom";
import DropdownSelect from "../components/shared/DropdownSelect";
import { DropdownSelectOption } from "../types/DropdownSelectOption";
import { render, screen, fireEvent } from "@testing-library/react";
import { v4 as uuidv4 } from "uuid";

describe("DropdownSelect", () => {
  const options: DropdownSelectOption[] = [
    { label: uuidv4(), value: "John Doe" },
    { label: uuidv4(), value: "Jane Smith" },
  ];

  const onChange = jest.fn();

  beforeEach(() => {
    render(
      <DropdownSelect
        label="Select an option"
        options={options}
        onChange={onChange}
      />
    );
  });

  it("renders the dropdown label", () => {
    const label = screen.getByText("Select an option");
    expect(label).toBeInTheDocument();
  });

  it("opens the dropdown when clicked", () => {
    const dropdown = screen.getByText("Select an option");
    fireEvent.click(dropdown);

    const option1 = screen.getByText(options[0].label);
    const option2 = screen.getByText(options[1].label);

    expect(option1).toBeInTheDocument();
    expect(option2).toBeInTheDocument();
  });

  it("calls onChange when an option is selected", () => {
    const dropdown = screen.getByText("Select an option");
    fireEvent.click(dropdown);

    const option1 = screen.getByText(options[0].label);
    fireEvent.click(option1);

    expect(onChange).toHaveBeenCalledWith(options[0].value);
  });
});
