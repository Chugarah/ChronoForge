import React from "react";
import { Switch } from "@/components/forms/switch";
import ButtonForm from "@/components/forms/ButtonForm";
import { faUser } from "@fortawesome/free-regular-svg-icons";

function NavGroup() {
	return (
		<div className="header__nav-group">
			{/* Theme Switch */}
			<span className="header__nav-group__text-span"> Dark Mode </span>
			<Switch />
			{/* Sign in Button */}
			<ButtonForm
				title="Sign up / Sign in"
				className="button-primary"
				iconPosition="left"
				icon={faUser}
				size="default"
				variant="default"
			/>
		</div>
	);
}

export default NavGroup;
