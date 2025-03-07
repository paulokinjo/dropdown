/**
 * DropdownSelect component renders a customizable dropdown select input.
 * 
 * @component
 * @param {DropwdownSelectProps} props - The properties for the DropdownSelect component.
 * @param {string} props.label - The label to display when no option is selected.
 * @param {Array<{ label: string, value: any }>} props.options - The list of options to display in the dropdown.
 * @param {function} props.onChange - The callback function to call when an option is selected.
 * 
 * @example
 * const options = [
 *   { label: 'Option 1', value: 1 },
 *   { label: 'Option 2', value: 2 },
 * ];
 * 
 * <DropdownSelect
 *   label="Select an option"
 *   options={options}
 *   onChange={(value) => console.log(value)}
 * />
 * 
 * @returns {JSX.Element} The rendered DropdownSelect component.
 */

import { DropdownSelectOption } from "@/app/types/DropdownSelectOption";
import { useState, useEffect, useRef } from "react";
import { BiChevronDown } from "react-icons/bi";
import { AiOutlineSearch } from "react-icons/ai";

interface DropwdownSelectProps {
  label: string;
  options: DropdownSelectOption[];
  onChange: (value: any) => void;
}

const DropdownSelect: React.FC<DropwdownSelectProps> = ({
  label,
  options,
  onChange,
}) => {
  const [inputValue, setInputValue] = useState("");
  const [open, setOpen] = useState(false);
  const [selected, setSelected] = useState("");
  const [highlightedIndex, setHighlightedIndex] = useState(-1);
  const listRef = useRef<HTMLUListElement>(null);

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === "ArrowDown") {
      setHighlightedIndex((prevIndex) =>
        prevIndex < options.length - 1 ? prevIndex + 1 : prevIndex
      );
    } else if (e.key === "ArrowUp") {
      setHighlightedIndex((prevIndex) =>
        prevIndex > 0 ? prevIndex - 1 : prevIndex
      );
    } else if (e.key === "Enter" && highlightedIndex >= 0) {
      const option = options[highlightedIndex];
      setSelected(option.label);
      setOpen(false);
      setInputValue("");
      onChange(option.value);
    }
  };

  useEffect(() => {
    if (open && listRef.current) {
      listRef.current.focus();
    }
  }, [open]);

  return (
    <div className="w-72 font-medium h-0">
      <div
        onClick={() => setOpen(!open)}
        className={`bg-white w-full p-2 flex items-center justify-between rounded ${
          !selected && "text-gray-700"
        }`}
      >
        {selected
          ? selected?.length > 35
            ? selected?.substring(0, 35) + "..."
            : selected
          : label}
        <BiChevronDown size={30} className={`${open && "rotate-180"}`} />
      </div>
      <ul
        ref={listRef}
        tabIndex={0}
        onKeyDown={handleKeyDown}
        className={`bg-white mt-2 overflow-y-auto ${
          open ? "max-h-60" : "max-h-0"
        } `}
      >
        <div className="flex items-center px-2 sticky top-0 bg-white z-10">
          <AiOutlineSearch size={18} className="text-gray-700" />
          <input
            type="text"
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value.toLowerCase())}
            placeholder="Enter a person's name"
            className="placeholder:text-gray-700 p-2 outline-none"
          />
        </div>
        {options?.map((option, index) => (
          <li
            key={option?.label}
            className={`p-2 text-sm hover:bg-[#9fc3f870] hover:text-black
          ${
            option?.label?.toLowerCase() === selected?.toLowerCase() &&
            "bg-[#0d6efd] text-white"
          }
          ${
            option?.label?.toLowerCase().includes(inputValue)
              ? "block"
              : "hidden"
          }
          ${highlightedIndex === index && "bg-gray-200"}`}
            onClick={() => {
              if (option?.label?.toLowerCase() !== selected.toLowerCase()) {
                setSelected(option?.label);
                setOpen(false);
                setInputValue("");
                onChange(option?.value);
              }
            }}
          >
            {option?.label}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default DropdownSelect;
