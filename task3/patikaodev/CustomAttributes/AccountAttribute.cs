namespace patikaodev.CustomAttributes {
	[AttributeUsage(AttributeTargets.Class)]
	public class AccountAttribute : Attribute { // Custom attribute for authorization.
		private string role;
		public AccountAttribute(string role) {
			this.role = role;
		}
		public string GetRole() {
			return role;
		}
	}
}